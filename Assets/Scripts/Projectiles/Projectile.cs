using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticles;
    [SerializeField] private int projectileDamage;

    public enum Initiators
    {
        player,
        enemy
    }

    private Initiators initiator;

    public void SetInitiator(Initiators passedInitiator)
    {
        initiator = passedInitiator;
    }

    public void SetDamage(int damage)
    {
        projectileDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (initiator)
        {
            case Initiators.player:
                if (collision.CompareTag("Enemy"))
                {
                    collision.GetComponent<EnemyHealthHandler>().TakeDamage(projectileDamage);
                }
                break;
            case Initiators.enemy:
                if (collision.CompareTag("Player"))
                {
                    PlayerStateTracker.DealDamage(projectileDamage);
                }
                break;
        }

        Destroy(gameObject);
    }
}
