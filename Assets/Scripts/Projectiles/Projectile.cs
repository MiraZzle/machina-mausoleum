using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticles;
    private int projectileDamage;
    void Update()
    {
        
    }

    public void SetDamage(int damage)
    {
        projectileDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealthHandler>().TakeDamage(projectileDamage);
        }
        Destroy(gameObject);

    }
}
