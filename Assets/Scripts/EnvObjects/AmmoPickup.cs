using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : KeyPickup
{
    [SerializeField] private int minAmmo = 10;
    [SerializeField] private int maxAmmo = 60;

    private int ammoHeld; 

    [SerializeField] GameObject weaponManager;
    void Awake()
    {
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager");
        // Set random amount of held ammo
        ammoHeld = Random.Range(minAmmo, maxAmmo);
    }

    // Find PlayerWeaponManager and increment held ammo
    protected override void ActOnPickup()
    {
        weaponManager.GetComponent<PlayerWeaponManager>().GetCurrentGun().GetComponent<GunShooting>().AddAmmo(ammoHeld);
    }
}
