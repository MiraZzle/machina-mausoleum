using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAxeManager : MonoBehaviour
{
    // The rotation speed of the bot axe in degrees / second
    [SerializeField] private int rotationSpeed = 70;
    [SerializeField] private int damage = 1;

    void Update()
    {
        // Rotate the bot axe around its z-axis over time
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStateTracker.DealDamage(damage);
        }
    }
}
