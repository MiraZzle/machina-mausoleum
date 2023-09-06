using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStateTracker
{
    public static int maxHealth = 8;
    public static int currentHealth = 3;

    public static bool keyObtained = false;

    public static List<GameObject> gunInventory;
    public static int currentGunIndex;
    public static GameObject currentGun;

    public static int enemiesKilled = 0;

    public delegate void OnDamageTaken();
    public static OnDamageTaken onDamageTaken;
    static void Start()
    {
        
    }
    static void Update()
    {

    }

    public static void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        onDamageTaken();
    }








}
