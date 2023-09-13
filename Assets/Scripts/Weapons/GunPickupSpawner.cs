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


    public void SpawnPickup(int amo)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 spawnOffset = (mousePosition - gameObject.transform.position).normalized;

        GameObject pickupInst = Instantiate(weaponPickup, gameObject.transform.position + spawnOffset * 6, Quaternion.identity);
        pickupInst.GetComponent<WeaponPickup>().SetHeldAmo(amo);
    }
}
