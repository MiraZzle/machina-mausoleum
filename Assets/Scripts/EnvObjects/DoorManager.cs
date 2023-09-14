using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Flag to track if the room is cleared - all enemies are dead
    public bool roomCleared = false;

    // Flag to track if the room was entered by player
    public bool roomEntered = false;

    void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        if (roomCleared)
        {
            animator.SetBool("roomCleared", true);
        }

        if (roomEntered)
        {
            animator.SetBool("roomEntered", true);
        }
    }
}
