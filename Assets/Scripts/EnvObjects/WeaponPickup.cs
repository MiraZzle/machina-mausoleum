using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : CratePickup
{
    [SerializeField] GameObject gunReference;
    private bool canPassAmo = false;

    // The current amount of ammo associated with this weapon
    public int currentAmo;

    void Start()
    {
        containedGun = gunReference;
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
            // Add the containedGun to the player's weapon manager.
            gunManager.GetComponent<PlayerWeaponManager>().AddGun(containedGun);

            if (canPassAmo)
            {
                // Set the current ammo amount for the added weapon
                gunManager.GetComponent<PlayerWeaponManager>().SetCurrentAmo(currentAmo);
            }

            PlaySoundEffect();

            Destroy(gameObject);
        }
    }

    // Set information based on already used gun
    public void SetHeldAmo(int amo)
    {
        canPassAmo = true;
        currentAmo = amo;
    }
}
