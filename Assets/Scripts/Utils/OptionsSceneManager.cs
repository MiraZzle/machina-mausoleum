using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject SFXToggle;
    [SerializeField] private GameObject musicToggle;
    [SerializeField] private GameObject transitionManager;

    void Start()
    {
        SFXToggle.GetComponent<Toggle>().isOn = OptionManager.SFXEnabled;
        musicToggle.GetComponent<Toggle>().isOn = OptionManager.musicEnabled;
    }

    public void EnableSFX(bool enabled)
    {
        OptionManager.SFXEnabled = enabled;
    }

    public void EnableMusic(bool enabled)
    {
        OptionManager.musicEnabled = enabled;
    }

    public void GoToMainMenu()
    {
        StartCoroutine(TransitionToMenu());
    }

    IEnumerator TransitionToMenu()
    {
        transitionManager.GetComponent<TransitionManager>().FadeIn();

        yield return new WaitForSecondsRealtime(OptionManager.transitionTime);

        LevelManager.LoadMenu();
    }
}
