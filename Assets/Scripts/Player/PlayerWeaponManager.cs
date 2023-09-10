using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerWeaponManager : MonoBehaviour
{

    private int currentGunIndex = 0;
    private int numberOfGuns = 1;

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



    void Start()
    {
        UpdateGuns();
        //LevelManager.levelChanged += SaveGuns;
        PlayerStateTracker.gunInventory.Append("Shotgun");

        LoadGuns();
    }

    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            SwitchWeapon(1);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            SwitchWeapon(-1);
        }

        ManageRolling();
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
        foreach (var gun in gunList)
        {
            PlayerStateTracker.gunInventory.Append(gun.gameObject.name);
        }
    }

    private void LoadGuns()
    {
        foreach (var item in PlayerStateTracker.gunInventory)
        {
            AddFromGunReferences(item);
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
