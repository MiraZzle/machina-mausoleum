using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    protected GameObject playerRef;
    private bool playerColliding = false;

    void Update()
    {
        CheckForPickup();
    }

    protected virtual void ActOnPickup()
    {
        PlayerStateTracker.keyObtained = true;
    }

    // Check for player input to pick up the key (E)
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

    // Detect when the player enters the trigger zone of the key pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerColliding = true;
        }
    }

    // Detect when the player exits the trigger zone of the key pickup.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerColliding = false;
        }
    }
}
