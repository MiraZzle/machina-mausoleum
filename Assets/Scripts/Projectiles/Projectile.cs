using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticles;
    [SerializeField] private int projectileDamage;

    // Enum to identify the initiator of the projectile (player or enemy
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
        // Determine how the projectile interacts with the collided object based on its initiator
        switch (initiator)
        {
            case Initiators.player:
                if (collision.CompareTag("Enemy"))
                {
                    // Deal damage to the enemy if the projectile hits
                    collision.GetComponent<EnemyHealthHandler>().TakeDamage(projectileDamage);
                }
                break;
            case Initiators.enemy:
                if (collision.CompareTag("Player"))
                {
                    // Deal damage to the player if the enemy's projectile hits
                    PlayerStateTracker.DealDamage(projectileDamage);
                }
                break;
        }

        Destroy(gameObject);
    }
}
