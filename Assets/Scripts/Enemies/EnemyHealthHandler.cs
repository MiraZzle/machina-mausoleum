using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    // Reference to the EnemyMovement script for melee enemies.
    [SerializeField] private EnemyMovement mover;

    // Reference to the EnemyMovement script for ranged enemies.
    [SerializeField] private ShootingEnemyMovement shootingMover;
    [SerializeField] private DamageFlasher flasher;

    // Reference to the AxeSpawner for disabling axes
    [SerializeField] private AxeSpawner axeSpawner;

    // The item spawn point associated with this enemy - used for "drop"
    [SerializeField] private GameObject itemSpawnPoint;
    [SerializeField] private bool isRanged = true;
    [SerializeField] private int maxHealth = 6;

    private int currentHealth;

    // Flag to prevent multiple death registrations
    private bool deathRegistered = false;


    void Start()
    {
        mover = GetComponent<EnemyMovement>();
        flasher = GetComponent<DamageFlasher>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth >= 0)
        {
            currentHealth -= damage;

            // Flash to indicate taking damage
            flasher.FlashOnDamage();
        }
        else
        {
            if (!isRanged)
            {
                axeSpawner.DisableAxes();
            }

            if (!deathRegistered)
            {
                // Spawn an item spawn point at the enemy's position
                InstantiateItemSpawnPoint(); 
                PlayerStateTracker.enemiesKilled++;
                flasher.FlashOnDamage();
                if (!isRanged)
                {
                    // Trigger death animation and behavior for melee enemies
                    mover.Die();
                }
                else
                {
                    shootingMover.Die();
                }
                transform.parent.GetComponent<EnemySpawner>().EnemyKilled();
            }
            // Mark death as registered to prevent multiple registrations
            deathRegistered = true;
        }
    }

    private void InstantiateItemSpawnPoint()
    {
        GameObject spawnPoint = Instantiate(itemSpawnPoint, transform.position, Quaternion.identity);
        spawnPoint.transform.parent = transform.parent.GetComponent<EnemySpawner>().GetItemSpawner().transform;
    }
}
