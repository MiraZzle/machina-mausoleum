using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject weaponPickup;
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void SpawnPickup()
    {
        GameObject pickupInst = Instantiate(weaponPickup, gameObject.transform.position, Quaternion.identity);
    }
}
