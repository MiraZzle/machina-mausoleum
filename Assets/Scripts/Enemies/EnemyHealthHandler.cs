using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField] private GameObject itemSpawnPoint;
    [SerializeField] private bool isRanged = true;
    [SerializeField] private AxeSpawner axeSpawner;
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private DamageFlasher flasher;
    private int currentHealth;
    private bool deathRegitered = false;

    [SerializeField] private EnemyMovement mover;
    [SerializeField] private ShootingEnemyMovement shootingMover;

    void Start()
    {
        mover = GetComponent<EnemyMovement>();
        flasher = GetComponent<DamageFlasher>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth >= 0)
        {
            currentHealth -= damage;
            flasher.FlashOnDamage();
        }
        else
        {
            if (!isRanged)
            {
                axeSpawner.DisableAxes();
            }

            if (!deathRegitered)
            {
                InstantiateItemSpawnPoint();
                PlayerStateTracker.enemiesKilled++;
                flasher.FlashOnDamage();
                if (!isRanged)
                {

                    mover.Die();
                }
                else
                {
                    shootingMover.Die();
                }
                transform.parent.GetComponent<EnemySpawner>().EnemyKilled();
            }

            deathRegitered = true;
        }
    }

    private void InstantiateItemSpawnPoint()
    {
        GameObject spawnPoint = Instantiate(itemSpawnPoint, transform.position, Quaternion.identity);
        spawnPoint.transform.parent = transform.parent.GetComponent<EnemySpawner>().GetItemSpawner().transform;
    }
}
