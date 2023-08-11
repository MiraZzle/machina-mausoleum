using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    [SerializeField] private float reloadTime;

    [SerializeField] private float cooldown;
    [SerializeField] private bool isAutomatic;

    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float damage;




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePos.position, firePos.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePos.right * projectileSpeed, ForceMode2D.Impulse);
    }

    void AutomaticShoot()
    {
    
    }

}
