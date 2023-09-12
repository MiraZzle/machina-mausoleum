using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorManager : MonoBehaviour
{
    public bool elevatorEntry = false;

    public bool elevatorActivated = false;
    public bool playerDetected = false;

    [SerializeField] private Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

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
