using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStateTracker
{
    public static float maxHealth = 100f;
    public static float currentHealth;

    public static bool keyObtained = false;

    public static List<GameObject> gunInventory;
    public static int currentGunIndex;
    public static GameObject currentGun;

    public static int enemiesKilled = 0;
    static void Start()
    {
        
    }
    static void Update()
    {
        Debug.Log(keyObtained);
    }






}
