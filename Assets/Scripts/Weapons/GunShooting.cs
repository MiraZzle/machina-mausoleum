using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class GunShooting : MonoBehaviour
{
    [SerializeField] private int accuracyDeg;
    [SerializeField] private int shotsPerFire = 1;

    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private bool automatic;
    [SerializeField] private bool canShoot;

    [SerializeField] private float reloadTime;
    [SerializeField] private float cooldown;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;

    private float shootReadyTime = 0f;

    public int maxAmo;
    public int currentAmo;


    void Start()
    {
        currentAmo = maxAmo;
    }

    void Update()
    {
        CheckIfShooting();
        currentAmo = math.clamp(currentAmo, 0, maxAmo);
    }

    private void CheckIfShooting()
    {
        if (Input.GetMouseButtonDown(0) && !automatic)
        {
            for (int i = 0; i < shotsPerFire; i++)
            {
                if (currentAmo > 0)
                {
                    Shoot();
                }
            }
        }

        if (Input.GetMouseButton(0) && automatic)
        {
            AutomaticShoot();
        }
    }

    private void Shoot()
    {
        DecreaseAmo();

        int bulletDeviation = UnityEngine.Random.Range(-accuracyDeg, accuracyDeg);
        Vector3 spread = new Vector3(0, 0, bulletDeviation);

        GameObject projectile = Instantiate(projectilePrefab, firePos.position, Quaternion.Euler(firePos.rotation.eulerAngles + spread));
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        rb.AddForce(projectile.transform.right * projectileSpeed, ForceMode2D.Impulse);

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
            for (int i = 0; i < shotsPerFire; i++)
            {
                if (currentAmo > 0)
                {
                    Shoot();
                }
            }
        }
    }

    private void DecreaseAmo()
    {
        currentAmo--;
    }

    public void AddAmmo(int amount)
    {
        currentAmo += amount;
    }

    public void SetAmo(int amo)
    {
        Debug.Log(amo);
        currentAmo = amo;
    }
}
