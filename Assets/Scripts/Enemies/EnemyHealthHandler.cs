using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField] private AxeSpawner axeSpawner;
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private DamageFlasher flasher;
    private int currentHealth;

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
            mover.Die();
            axeSpawner.DisableAxes();
            transform.parent.GetComponent<EnemySpawner>().EnemyKilled();
        }
    }
}
