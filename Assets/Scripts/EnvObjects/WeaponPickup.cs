using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : CratePickup
{
    [SerializeField] GameObject gunReference;
    [SerializeField] float throwDistance;
    [SerializeField] float throwSpeed;

    public int currentAmo;



    void Start()
    {
        containedGun = gunReference;
        Debug.Log(currentAmo);
    }

    void Update()
    {
        CheckForPickup();
    }

    protected void CheckForPickup()
    {
        if (playerColliding && Input.GetKeyDown(KeyCode.E))
        {
            GameObject gunManager = GameObject.FindGameObjectWithTag("WeaponManager");
            gunManager.GetComponent<PlayerWeaponManager>().AddGun(containedGun);
            gunManager.GetComponent<PlayerWeaponManager>().SetCurrentAmo(currentAmo);

            Debug.Log(gunManager.GetComponent<PlayerWeaponManager>());

            Destroy(gameObject);
        }
    }
}
