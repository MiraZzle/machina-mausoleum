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
    [SerializeField] private PlayerSFXManager SFXManager;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject weaponManager;

    // Reference to the currently equipped gun
    [SerializeField] private GameObject currentGun;
    [SerializeField] private List<GameObject> gunList;

    // Maximum number of guns the player can carry
    [SerializeField] private int maxGunCount = 3;

    private int currentGunIndex = 0;

    // Number of guns the player has
    private int numberOfGuns = 1;

    // Input axis for switching guns
    private string triggerAxis = "Mouse ScrollWheel";

    private string startingWeapon = "Pistol";

    [Serializable]
    public struct gunPair
    {
        public string gunName;
        public GameObject gunRef;
    }

    public gunPair[] gunPairs;

    void Start()
    {
        SFXManager = player.GetComponent<PlayerSFXManager>();
        weaponManager = gameObject;
        if (LevelManager.currentLevel == 1)
        {
            AddFromGunReferences(startingWeapon);
            UpdateGuns();
        }

        // Subscribe to the levelChanged event to save guns when the level changes
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

    // Allow the player to switch between equipped guns
    private void ScrollThroughGuns()
    {
        // Check for scrolling down
        if (Input.GetAxis(triggerAxis) < 0)
        {
            if (gunList.Count > 1)
            {
                SFXManager.PlayGunChangeSGX();
            }
            SwitchWeapon(1);
        }

        // Check for scrolling up
        if (Input.GetAxis(triggerAxis) > 0)
        {
            if (gunList.Count > 1)
            {
                SFXManager.PlayGunChangeSGX();
            }
            SwitchWeapon(-1);
        }
    }

    // Get the reference to the currently equipped gun
    public GameObject GetCurrentGun()
    {
        return currentGun;
    }

    public void SetCurrentAmo(int amo)
    {
        currentGun.GetComponent<GunShooting>().currentAmo = amo;
    }

    // Disable the currently equipped gun while rolling
    void ManageRolling()
    {
        currentGun.SetActive(!player.rolling);
    }

    // Switch to the next or previous gun in the list
    void SwitchWeapon(int indexChange)
    {
        UpdateGuns();

        gunList[currentGunIndex].SetActive(false);

        // Check for cycling to the end of the gun list
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

    // Update the list of available guns and their visibility
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
        weaponManager = gameObject;
        return weaponManager.transform.childCount;
    }

    // Add a new gun to the player's inventory
    public void AddGun(GameObject newGun)
    {

        GameObject addedGun = Instantiate(newGun, this.transform.position, this.transform.rotation);
        addedGun.transform.parent = weaponManager.transform;
        addedGun.GetComponent<GunShooting>().SetOwner(true);

        // If the player has not reached the maximum gun count, simply switch to the new gun
        if (numberOfGuns < maxGunCount)
        {
            UpdateGuns();
            SwitchWeapon(numberOfGuns -  currentGunIndex - 1);
        }

        else
        {
            // Replace the current gun with the new one, dropping the current gun as a pickup
            currentGun.GetComponent<GunPickupSpawner>().SpawnPickup(currentGun.GetComponent<GunShooting>().currentAmo);
            Destroy(currentGun); 
            currentGun = addedGun;

            // Remove the old gun from the list and adjust its position
            gunList.RemoveAt(currentGunIndex);
            addedGun.transform.SetSiblingIndex(currentGunIndex);
            SwitchWeapon(0);

            UpdateGuns();
        }
    }

    // Save the player's currently equipped guns and their ammo to the player state
    public void SaveGuns()
    {
        UpdateGuns();
        numberOfGuns = getNumberOfGuns();
        PlayerStateTracker.gunSafe.Clear();
        for (int i = 0; i < gunList.Count; i++)
        {
            PlayerStateTracker.SaveGun(
                gunList[i].GetComponent<GunShooting>().currentAmo, 
                gunList[i].GetComponent<Gun>().gunName
                );
        }
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

    // Add guns to the player's inventory based on predefined gun pairs
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
