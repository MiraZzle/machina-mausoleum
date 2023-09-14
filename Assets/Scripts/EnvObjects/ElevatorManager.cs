using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Flag to track if the elevator is an entrance
    public bool elevatorEntry = false;

    // Flag to track if the elevator is activated
    public bool elevatorActivated = false;
    public bool playerDetected = false;

    public void SetState(bool isEntrance)
    {
        elevatorEntry = isEntrance;
        animator.SetBool("isEntrance", elevatorEntry);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerStateTracker.keyObtained)
        {
            if (!elevatorEntry)
            {
                animator.SetBool("unlocked", true);
                // Consume the key in PlayerStateTracker
                PlayerStateTracker.keyObtained = false;
                LevelManager.LoadLevel();
            }
        }
    }

    public void OpenElevator()
    {
        animator.SetTrigger("OpenElevator");
    }
}
