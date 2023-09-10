using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStateTracker
{
    public static int maxHealth = 8;
    public static int currentHealth = maxHealth;

    public static bool keyObtained = false;

    public static string[] gunInventory;


    public static int currentGunIndex;
    public static GameObject currentGun;

    public static int enemiesKilled = 0;

    public delegate void OnDamageTaken();
    public static OnDamageTaken onDamageTaken;

    public delegate void PlayerDied();
    public static PlayerDied playerDied;

    public static bool playerVulnerable = true;
    public static bool dead = false;

    static void Start()
    {
        
    }
    static void Update()
    {

    }

    public static void DealDamage(int amount)
    {
        if (playerVulnerable)
        {
            ChangeHealth(-amount);
        }
    }

    public static void HealPlayer(int amount)
    {
        ChangeHealth(amount);
    }

    public static void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        onDamageTaken();

        if (currentHealth <= 0)
        {
            dead = true;
            playerDied();
        }
    }

    public static void ReloadGame()
    {
        GameStateManager.gamePaused = false;
        dead = false;
        currentHealth = maxHealth;
        enemiesKilled = 0;
        keyObtained = false;
    }

    public static void NullDelegates()
    {
        onDamageTaken = null;
        playerDied = null;
    }
}
