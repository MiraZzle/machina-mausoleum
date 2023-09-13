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

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private bool automatic;
    [SerializeField] private bool canShoot;

    [SerializeField] private float reloadTime;
    [SerializeField] private float cooldown;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private int damage;
    [SerializeField] private float fireRate;
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] private float shootRadius;

    [SerializeField] private bool ownedByPlayer = true;
    [SerializeField] private int damageByPlayer;
    [SerializeField] private int damageByEnemy;

    private float shootReadyTime = 0f;

    public int maxAmo;
    public int currentAmo;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentAmo = maxAmo;
    }

    void Start()
    {
        if (ownedByPlayer)
        {
            damage = damageByPlayer;
        }
        else
        {
            damage = damageByEnemy;
        }
    }

    void Update()
    {
        if (!GameStateManager.gamePaused)
        {
            if (ownedByPlayer)
            {
                CheckIfShooting();
            }
        }

        currentAmo = math.clamp(currentAmo, 0, maxAmo);
    }

    public void SetOwner(bool playerAsOwner)
    {
        ownedByPlayer = playerAsOwner; ;
    }

    public float GetRadius()
    {
        return shootRadius;
    }

    public float GetCooldown()
    {
        return cooldown;
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

    private void BaseShoot(Projectile.Initiators initiator, int projectileLayer)
    {
        //audioSource.PlayOneShot(shootingClip);

        int bulletDeviation = UnityEngine.Random.Range(-accuracyDeg, accuracyDeg);
        Vector3 spread = new Vector3(0, 0, bulletDeviation);

        GameObject projectile = Instantiate(projectilePrefab, firePos.position, Quaternion.Euler(firePos.rotation.eulerAngles + spread));
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        projectile.layer = projectileLayer;
        projectileScript.SetDamage(damage);

        projectileScript.SetInitiator(initiator);

        rb.AddForce(projectile.transform.right * projectileSpeed, ForceMode2D.Impulse);

        if (firePos.position.x < transform.position.x)
        {
            SpriteRenderer projectileSprite = projectile.GetComponent<SpriteRenderer>();
            projectileSprite.flipY = true;
        }
    }

    public void EnemyShooting()
    {
        int enemyProjectileLayer = LayerMask.NameToLayer("EnemyProjectile");
        BaseShoot(Projectile.Initiators.enemy, enemyProjectileLayer);
    }
    private void Shoot()
    {
        int playerProjectileLayer = LayerMask.NameToLayer("PlayerProjectile");
        DecreaseAmo();
        BaseShoot(Projectile.Initiators.player, playerProjectileLayer);
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
        currentAmo = amo;
    }
}
