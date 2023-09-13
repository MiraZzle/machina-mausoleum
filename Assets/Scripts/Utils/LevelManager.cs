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

    private static string menuName = "MainMenu";
    private static string bossLevelName = "BossLevel";
    private static string normalLevelName = "GunRange";



    static void Update()
    {
        
    }

    public static void LoadLevel()
    {
        levelChanged();

        levelChanged = null;
        PlayerStateTracker.playerVulnerable = true;
        PlayerStateTracker.NullDelegates();

        GameStateManager.gamePaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentLevel++;
    }

    public static void ExitGame()
    {
        Application.Quit();
    }

    public static void StartGame()
    {
        levelChanged = null;
        ReloadGame();
        PlayerStateTracker.NullDelegates();
        SceneManager.LoadScene(normalLevelName);
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene(menuName);
    }

    public static void ReloadGame()
    {
        levelChanged = null;
        GameStateManager.gamePaused = false;
        Time.timeScale = 1;
        currentLevel = 1;
        PlayerStateTracker.NullDelegates();
        PlayerStateTracker.ReloadGame();
    }
}
