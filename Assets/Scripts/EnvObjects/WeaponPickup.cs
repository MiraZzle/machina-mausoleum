using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : CratePickup
{
    [SerializeField] GameObject gunReference;
    private bool canPassAmo = false;

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
            gunManager.GetComponent<PlayerWeaponManager>().AddGun(containedGun);

            if (canPassAmo)
            {
                gunManager.GetComponent<PlayerWeaponManager>().SetCurrentAmo(currentAmo);
            }

            PlaySoundEffect();

            Destroy(gameObject);
        }
    }

    public void SetHeldAmo(int amo)
    {
        canPassAmo = true;
        currentAmo = amo;
    }
}
