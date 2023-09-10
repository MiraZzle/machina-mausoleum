using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject transitionManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(TransitionToGame());
        //LevelManager.StartGame();
    }

    public void EnterSettings()
    {

    }

    public void ExitGame()
    {
        LevelManager.ExitGame();
    }

    IEnumerator TransitionToGame()
    {
        transitionManager.GetComponent<TransitionManager>().FadeIn();

        yield return new WaitForSecondsRealtime(OptionManager.transitionTime);
        
        LevelManager.StartGame();
    }
}
