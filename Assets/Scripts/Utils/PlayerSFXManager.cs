using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXManager : MonoBehaviour
{
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip rollClip;
    [SerializeField] private AudioClip gunChangeClip;
    [SerializeField] private AudioClip gameOver;

    [SerializeField] private SoundManager soundManager;
    void Start()
    {
        soundManager = GetComponent<SoundManager>();   
    }

    public void PlayRollSFX()
    {
        soundManager.PlayEffect(rollClip);
    }

    public void PlayPickupSFX()
    {
        soundManager.PlayEffect(pickupClip);
    }

    public void PlayGunChangeSGX()
    {
        soundManager.PlayEffect(gunChangeClip);
    }

    public void PlayGameOver()
    {
        soundManager.PlayEffect(gameOver);
    }


}
