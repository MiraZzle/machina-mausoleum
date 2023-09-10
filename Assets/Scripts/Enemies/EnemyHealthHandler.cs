using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField] private bool isRanged = true;
    [SerializeField] private AxeSpawner axeSpawner;
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private DamageFlasher flasher;
    private int currentHealth;
    private bool died = false;

    [SerializeField] private EnemyMovement mover;

    void Start()
    {
        mover = GetComponent<EnemyMovement>();
        flasher = GetComponent<DamageFlasher>();
        //axeSpawner = GetComponent<AxeSpawner>();

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

            if (!died)
            {
                PlayerStateTracker.enemiesKilled++;
                flasher.FlashOnDamage();
                mover.Die();
                transform.parent.GetComponent<EnemySpawner>().EnemyKilled();
            }

            died = true;

            //Invoke("DestroyOnDeath", 5);
        }
    }

    private void DestroyOnDeath()
    {
        Destroy(gameObject);
    }
}
