using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    protected GameObject playerRef;
    private bool playerColliding = false;
    void Start()
    {
        
    }
    void Update()
    {
        CheckForPickup();
    }

    protected virtual void ActOnPickup()
    {
        PlayerStateTracker.keyObtained = true;
    }

    protected void CheckForPickup()
    {
        if (playerColliding && Input.GetKeyDown(KeyCode.E))
        {
            ActOnPickup();
            PlaySoundEffect();
            Destroy(gameObject);
        }
    }

    protected void PlaySoundEffect()
    {
        playerRef = GameObject.FindWithTag("Player");
        playerRef.GetComponent<PlayerSFXManager>().PlayPickupSFX();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerColliding = false;
        }
    }
}
