using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : KeyPickup
{
    [SerializeField] private int minAmmo = 10;
    [SerializeField] private int maxAmmo = 60;

    private int ammoHeld; 

    [SerializeField] GameObject weaponManager;
    void Start()
    {
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager");
        ammoHeld = Random.Range(minAmmo, maxAmmo);
    }


    protected override void ActOnPickup()
    {
        weaponManager.GetComponent<PlayerWeaponManager>().GetCurrentGun().GetComponent<GunShooting>().AddAmmo(ammoHeld);
    }
}
