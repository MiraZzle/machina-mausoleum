using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{

    private int currentGunIndex = 0;
    private int numberOfGuns = 1;

    //[SerializeField] private int maxGunSlots = 2;
    [SerializeField] private PlayerMovement player;


    [SerializeField] private GameObject weaponManager;
    [SerializeField] private GameObject currentGun;

    [SerializeField] private GameObject[] gunList;



    void Start()
    {
        getGuns();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switchWeapon(1);
        }

        if (player.rolling)
        {
            currentGun.SetActive(false);
        }
        else
        {
            currentGun.SetActive(true);
        }

    }

    void switchWeapon(int indexChange)
    {
        gunList[currentGunIndex].SetActive(false);

        currentGunIndex += indexChange;

        currentGunIndex %= numberOfGuns;

        gunList[currentGunIndex].SetActive(true);

        currentGun = gunList[currentGunIndex];
    }

    void getGuns()
    {
        numberOfGuns = weaponManager.transform.childCount;
        gunList = new GameObject[numberOfGuns];

        for (int i = 0; i < numberOfGuns; i++)
        {
            gunList[i] = weaponManager.transform.GetChild(i).gameObject;
            gunList[i].SetActive(false);
        }

        gunList[currentGunIndex].SetActive(true);
        currentGun = gunList[currentGunIndex];

    }
}
