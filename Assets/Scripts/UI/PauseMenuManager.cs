using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] protected GameObject transitionManager;
    [SerializeField] protected GameObject buttonHandler;
    [SerializeField] protected bool isPausable = true;
    void Start()
    {
        buttonHandler.SetActive(false);
    }

    void Update()
    {
        if (isPausable && !PlayerStateTracker.dead)
        {
            CheckForPause();
        }
    }

    private void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameStateManager.gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    protected void PauseGame()
    {
        Time.timeScale = 0;
        GameStateManager.gamePaused = true;
        buttonHandler.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameStateManager.gamePaused = false;
        buttonHandler.SetActive(false);
    }

    public void ExitGame()
    {
        LevelManager.ExitGame();
    }

    public void GoToMenu()
    {
        //LevelManager.LoadMenu();
        StartCoroutine(ChangeToMenu());
        //ChangeToMenu();
    }

    IEnumerator ChangeToMenu()
    {
        transitionManager.GetComponent<TransitionManager>().FadeIn();

        yield return new WaitForSecondsRealtime(OptionManager.transitionTime);

        LevelManager.LoadMenu();

        GameStateManager.gamePaused = false;
        Time.timeScale = 1;

    }
}
