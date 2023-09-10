using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenuManager : PauseMenuManager
{
    [SerializeField] private GameObject captionDisplayer;
    [SerializeField] private GameObject scoreDisplayer;

    public List<string> deathCaptions = new List<string> { 
        "Time to wind down, adventurer.",
        "Toast is burnt, and so is your luck.",
        "Your library of life has been closed.",
        "In this labyrinth, you've met your final twist.",
        "Your adventure has run out of steam.",
        "You've been served a steaming defeat."
    };
    void Start()
    {
        buttonHandler.SetActive(false);
        PlayerStateTracker.playerDied += DisplayMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string ChooseCaption()
    {
        return deathCaptions[Random.Range(0, deathCaptions.Count)];
    }

    public void DisplayMenu()
    {
        string caption = '"' + ChooseCaption().ToUpper() + '"';
        buttonHandler.SetActive(true);

        captionDisplayer.GetComponent<TextMeshProUGUI>().text = caption;
        scoreDisplayer.GetComponent<TextMeshProUGUI>().text =
            "Score: " + PlayerStateTracker.enemiesKilled.ToString();

        PauseGame();
    }

    public void RestartGame()
    {
        StartCoroutine(ReloadGame());
        //ReloadGame();
    }

    IEnumerator ReloadGame()
    {
        transitionManager.GetComponent<TransitionManager>().FadeIn();

        yield return new WaitForSecondsRealtime(OptionManager.transitionTime);

        LevelManager.StartGame();
    }
}
