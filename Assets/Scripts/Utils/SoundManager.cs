using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool themePlayer = false;
    [SerializeField] private bool effectPlayer = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayTheme();
    }

    private void Update()
    {
        OnThemePause();
        StopOnGameOver();
    }

    private void OnThemePause()
    {
        if (!OptionManager.musicEnabled && themePlayer)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    private void StopOnGameOver()
    {
        if (themePlayer && PlayerStateTracker.dead)
        {
            audioSource.Pause();
        }
    }

    public void PlayEffect(AudioClip clipToPlay)
    {
        if (OptionManager.SFXEnabled && effectPlayer)
        {
            audioSource.PlayOneShot(clipToPlay);
        }
    }

    private void PlayTheme()
    {
        if (OptionManager.musicEnabled && themePlayer)
        {
            audioSource.Play();
        }
    }

}
