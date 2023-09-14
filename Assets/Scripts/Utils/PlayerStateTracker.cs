using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStateTracker
{
    public static int maxHealth = 8;
    public static int currentHealth = maxHealth;
    public static bool keyObtained = false;
    public static List<GunInfo> gunSafe = new List<GunInfo>();
    public static string gunName = "Shotgun";
    public static int currentGunIndex;
    public static int enemiesKilled = 0;

    // Delegate to handle damage taken by the player.
    public delegate void OnDamageTaken();
    public static OnDamageTaken onDamageTaken;

    // Delegate to handle the player's death.
    public delegate void PlayerDied();
    public static PlayerDied playerDied;

    public static bool playerVulnerable = true;
    public static bool dead = false;

    // Struct to hold a reference to a gun GameObject
    public struct GunRef
    {
        public GameObject gRef;
    }

    public struct GunInfo
    {
        public string gunName;
        public int ammo;
    }

    public static void SaveGun(int passedAmmo, string passedName)
    {
        GunInfo gunI = new GunInfo();
        gunI.gunName = passedName;
        gunI.ammo = passedAmmo;

        gunSafe.Add(gunI);
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
