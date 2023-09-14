using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenuManager : PauseMenuManager
{
    [SerializeField] private GameObject captionDisplayer;
    [SerializeField] private GameObject scoreDisplayer;

    // List of death captions displayed randomly when the player dies
    public List<string> deathCaptions = new List<string> { 
        "Time to wind down, adventurer.",
        "Toast is burnt, and so is your luck.",
        "Your library of life has been closed.",
        "In this labyrinth, you've met your final twist.",
        "Your adventure has run out of steam.",
        "You've been served a steaming defeat."
    };

    private char quoteChar = '"';
    void Start()
    {
        buttonHandler.SetActive(false);
        // Subscribe to the playerDied event to display the death menu
        PlayerStateTracker.playerDied += DisplayMenu;
    }

    // Randomly choose a death caption from the list
    private string ChooseCaption()
    {
        return deathCaptions[Random.Range(0, deathCaptions.Count)];
    }

    public void DisplayMenu()
    {
        // Format the chosen caption for display
        string caption = quoteChar + ChooseCaption().ToUpper() + quoteChar;
        buttonHandler.SetActive(true);

        // Display the player's score (number of enemies killed)
        captionDisplayer.GetComponent<TextMeshProUGUI>().text = caption;
        scoreDisplayer.GetComponent<TextMeshProUGUI>().text =
            "Score: " + PlayerStateTracker.enemiesKilled.ToString();

        PauseGame();
    }

    public void RestartGame()
    {
        StartCoroutine(ReloadGame());
    }

    IEnumerator ReloadGame()
    {
        transitionManager.GetComponent<TransitionManager>().FadeIn();

        yield return new WaitForSecondsRealtime(OptionManager.transitionTime);

        LevelManager.StartGame();
    }
}
