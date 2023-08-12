using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunShooting : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Animator gunAnimator;

    [SerializeField] private bool automatic;
    [SerializeField] private bool canShoot;

    [SerializeField] private float reloadTime;
    [SerializeField] private float cooldown;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;

    [SerializeField] private float accuracyDeg;



    private float shootReadyTime = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !automatic)
        {
            Shoot();
        }

        if (Input.GetMouseButton(0) && automatic)
        {
            AutomaticShoot();
        }
    }

    private void Shoot()
    {

        GameObject projectile = Instantiate(projectilePrefab, firePos.position, firePos.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        rb.AddForce(firePos.right * projectileSpeed, ForceMode2D.Impulse);

        if (firePos.position.x < transform.position.x)
        {
            SpriteRenderer projectileSprite = projectile.GetComponent<SpriteRenderer>();
            projectileSprite.flipY = true;
        }
    }

    private void AutomaticShoot()
    {
        if (Time.time > shootReadyTime)
        {
            shootReadyTime = Time.time + 1/fireRate;
            Shoot();
        }
    }

}
