using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticles;
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            //GameObject explosionInst = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
