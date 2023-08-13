using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : CratePickup
{
    [SerializeField] GameObject gunReference;
    [SerializeField] float throwDistance;
    [SerializeField] float throwSpeed;


    void Start()
    {
        containedGun = gunReference;
    }

    void Update()
    {
        CheckForPickup();
    }
}
