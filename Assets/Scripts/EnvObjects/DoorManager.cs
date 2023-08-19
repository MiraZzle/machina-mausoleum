using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public bool roomCleared = false;
    public bool roomEntered = false;

    void Start()
    {
        
    }

    void Update()
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
