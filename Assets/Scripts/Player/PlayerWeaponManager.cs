using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerWeaponManager : MonoBehaviour
{

    private int currentGunIndex = 0;
    private int numberOfGuns = 1;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private PlayerMovement player;

    [SerializeField] private GameObject weaponManager;
    [SerializeField] private GameObject currentGun;

    [SerializeField] private List<GameObject> gunList;

    [SerializeField] private int maxGunCount = 3;

    [Serializable]
    public struct gunPair
    {
        public string gunName;
        public GameObject gunRef;
    }

    public gunPair[] gunPairs;

    [SerializeField] private AudioClip shootingClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (LevelManager.currentLevel == 1)
        {
            AddFromGunReferences("Pistol");
            UpdateGuns();
        }

        LevelManager.levelChanged += SaveGuns;



        if (LevelManager.currentLevel != 1)
        {
            LoadGuns();
            UpdateGuns();
            Debug.Log(PlayerStateTracker.currentGunIndex);
            SwitchWeapon(PlayerStateTracker.currentGunIndex);
        }
    }

    void Update()
    {
        ScrollThroughGuns();
        ManageRolling();
    }

    private void ScrollThroughGuns()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (gunList.Count > 1)
            {
                audioSource.Play();
            }
            SwitchWeapon(1);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (gunList.Count > 1)
            {
                audioSource.Play();
            }
            SwitchWeapon(-1);
        }
    }

    public GameObject GetCurrentGun()
    {
        return currentGun;
    }

    public void SetCurrentAmo(int amo)
    {
        currentGun.GetComponent<GunShooting>().SetAmo(amo);
    }

    void ManageRolling()
    {
        if (player.rolling)
        {
            currentGun.SetActive(false);
        }
        else
        {
            currentGun.SetActive(true);
        }
    }

    void SwitchWeapon(int indexChange)
    {
        UpdateGuns();

        gunList[currentGunIndex].SetActive(false);

        if (currentGunIndex + indexChange < 0)
        {
            currentGunIndex = numberOfGuns - 1;
        }

        else
        {

            currentGunIndex += indexChange;

            currentGunIndex %= numberOfGuns;
        }


        gunList[currentGunIndex].SetActive(true);

        currentGun = gunList[currentGunIndex];
    }

    private void UpdateGuns()
    {
        numberOfGuns = getNumberOfGuns();
        gunList = new List<GameObject>();

        for (int i = 0; i < numberOfGuns; i++)
        {
            gunList.Add(weaponManager.transform.GetChild(i).gameObject);
            gunList[i].SetActive(false);
        }

        gunList[currentGunIndex].SetActive(true);
        currentGun = gunList[currentGunIndex];
    }

    private int getNumberOfGuns()
    {
        return weaponManager.transform.childCount;
        
    }

    public void AddGun(GameObject newGun)
    {

        GameObject addedGun = Instantiate(newGun, this.transform.position, this.transform.rotation);
        addedGun.transform.parent = weaponManager.transform;


        if (numberOfGuns < maxGunCount)
        {
            UpdateGuns();
            SwitchWeapon(numberOfGuns -  currentGunIndex - 1);
        }

        else
        {
            currentGun.GetComponent<GunPickupSpawner>().SpawnPickup(currentGun.GetComponent<GunShooting>().currentAmo);

            Destroy(currentGun); 
            currentGun = addedGun;

            gunList.RemoveAt(currentGunIndex);

            addedGun.transform.SetSiblingIndex(currentGunIndex);
            SwitchWeapon(0);

            UpdateGuns();
        }
    }

    
    // nebo singletion class
    // use pair key - prefab! -> tady budes asi muset vyuzit neco jineho nez list nebo array
    public void SaveGuns()
    {
        UpdateGuns();
        PlayerStateTracker.gunSafe.Clear();
        for (int i = 0; i < gunList.Count; i++)
        {
            PlayerStateTracker.SaveGun(
                gunList[i].GetComponent<GunShooting>().currentAmo, 
                gunList[i].GetComponent<Gun>().gunName
                );
        }
        Debug.Log(currentGunIndex);
        PlayerStateTracker.currentGunIndex = currentGunIndex;
    }

    private void LoadGuns()
    {
        for (int i = 0; i < PlayerStateTracker.gunSafe.Count; i++)
        {
            AddFromGunReferences(PlayerStateTracker.gunSafe[i].gunName);
            gunList[i].GetComponent<GunShooting>().currentAmo = PlayerStateTracker.gunSafe[i].ammo;
        }
    }

    private void AddFromGunReferences(string nameToAdd)
    {
        foreach (var pair in gunPairs)
        {
            if (pair.gunName == nameToAdd)
            {
                AddGun(pair.gunRef);
            }
        }
    }
}
