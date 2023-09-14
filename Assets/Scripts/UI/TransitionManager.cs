using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        FadeOut();
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIN");
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOUT");
    }
}
