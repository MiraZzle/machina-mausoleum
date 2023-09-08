using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    public static int currentLevel = 1;
    private static int bossLevel = 5;

    public delegate void LevelChanged();
    public static LevelChanged levelChanged;
    static void Start()
    {
        
    }

    static void Update()
    {
        
    }

    public static void LoadLevel()
    {
        PlayerStateTracker.onDamageTaken = null;
        PlayerStateTracker.playerVulnerable = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentLevel++;
    }

    public static void LoadNextLevel()
    {
        levelChanged();
        PlayerStateTracker.onDamageTaken = null; // would throw null reference error

        currentLevel++;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        levelChanged = null;
    }

    public static void LoadMenu()
    {
    
    }

    public static void ReloadGame()
    {
        currentLevel = 1;
        PlayerStateTracker.ReloadGame();
    }
}
