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

    [SerializeField] private List<GameObject> gunList;

    [SerializeField] private int maxGunCount = 3;



    void Start()
    {
        UpdateGuns();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchWeapon(1);
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

    void SwitchWeapon(int indexChange)
    {
        UpdateGuns();

        gunList[currentGunIndex].SetActive(false);

        currentGunIndex += indexChange;

        currentGunIndex %= numberOfGuns;

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
            Destroy(currentGun); 
            currentGun = addedGun;

            gunList.RemoveAt(currentGunIndex);

            addedGun.transform.SetSiblingIndex(currentGunIndex);
            SwitchWeapon(0);

            UpdateGuns();
        }
    }
}
