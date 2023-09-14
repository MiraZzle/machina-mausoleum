using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject transitionManager;

    // Start the game when the StartBbutton is pressed
    public void StartGame()
    {
        StartCoroutine(TransitionToGame());
    }

    public void EnterSettings()
    {
        StartCoroutine(TransitionToOptions());
    }

    public void ExitGame()
    {
        LevelManager.ExitGame();
    }

    // Perform a transition to start the game
    IEnumerator TransitionToGame()
    {
        transitionManager.GetComponent<TransitionManager>().FadeIn();

        yield return new WaitForSecondsRealtime(OptionManager.transitionTime);
        
        LevelManager.StartGame();
    }

    IEnumerator TransitionToOptions()
    {
        transitionManager.GetComponent<TransitionManager>().FadeIn();

        yield return new WaitForSecondsRealtime(OptionManager.transitionTime);

        LevelManager.LoadOptionsMenu();
    }
}
