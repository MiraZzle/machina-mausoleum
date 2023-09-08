using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAxeManager : MonoBehaviour
{
    [SerializeField] private int rotationSpeed = 70;
    [SerializeField] private int damage = 1;

    void Start()
    {
        Debug.Log("Spawned");
    }

    void Update()
    {
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
