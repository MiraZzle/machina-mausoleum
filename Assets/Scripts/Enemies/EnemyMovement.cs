using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform playerTarget;

    [SerializeField] private Collider2D physicsCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public enum movementStates
    {
        moving,
        guarding,
        attacking,
        dead
    }

    public movementStates currentState;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetupAgent();
        currentState = movementStates.moving;
    }

    void Update()
    {
        StateLogic();
    }

    private void SetupAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void CheckForSpriteFlip()
    {
        spriteRenderer.flipX = !(agent.desiredVelocity.x > 0);
    }

    public void Die()
    {
        currentState = movementStates.dead;
        animator.SetTrigger("Died");
        physicsCollider.enabled = false;
    }

    private void StateLogic()
    {
        switch (currentState)
        {
            case movementStates.moving:
                agent.SetDestination(playerTarget.position);
                CheckForSpriteFlip();
                break;
            case movementStates.guarding:
                break;
            case movementStates.attacking: 
                break;
            case movementStates.dead:
                agent.enabled = false;
                break;
        }
    }

}
