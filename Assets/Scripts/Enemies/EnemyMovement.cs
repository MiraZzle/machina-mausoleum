using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    protected Transform playerTarget;
    public bool dead = false;

    [SerializeField] protected Collider2D physicsCollider;
    [SerializeField] protected Animator animator;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    public enum movementStates
    {
        moving,
        guarding,
        attacking,
        dead
    }

    protected movementStates currentState;

    protected void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetupAgent();
        currentState = movementStates.moving;
    }

    protected void Update()
    {
        StateLogic();
    }

    protected void SetupAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected void CheckForSpriteFlip()
    {
        spriteRenderer.flipX = !(agent.desiredVelocity.x > 0);
    }

    public void Die()
    {
        animator.SetTrigger("Died");
        physicsCollider.enabled = false;
        agent.enabled = false;
        ChangeState(movementStates.dead);
        dead = true;
    }

    public void ChangeState(movementStates targetState)
    {
        if (currentState != movementStates.dead)
        {
            currentState = targetState;
        }
    }

    protected virtual void StateLogic()
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
