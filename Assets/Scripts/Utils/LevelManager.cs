using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    private static string menuName = "MainMenu";
    private static string normalLevelName = "GunRange";
    private static string optionsMenuName = "OptionsMenu";
    public static int currentLevel = 1;

    // Event delegate to notify when the level changes
    public delegate void LevelChanged();
    public static LevelChanged levelChanged;

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

    public static void LoadOptionsMenu()
    {
        SceneManager.LoadScene(optionsMenuName);
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene(menuName);
        PlayerStateTracker.dead = false;
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
