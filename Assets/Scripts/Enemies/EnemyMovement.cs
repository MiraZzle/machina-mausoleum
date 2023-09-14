using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    protected Transform playerTarget;
    //public bool dead = false;

    [SerializeField] protected bool dead = false;

    [SerializeField] protected Collider2D physicsCollider;
    [SerializeField] protected Animator animator;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected GameObject weaponManager;

    public enum movementStates
    {
        moving,
        guarding,
        attacking,
        dead
    }

    [SerializeField] protected movementStates currentState;

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

    protected void CheckForSpriteFlipIdle()
    {
        spriteRenderer.flipX = !(playerTarget.transform.position.x > transform.position.x);
    }

    public void Die()
    {
        HandleDeath();
    }

    protected void HandleDeath()
    {
        dead = true;
        animator.SetTrigger("Died");

        physicsCollider.enabled = false;
        agent.enabled = false;


        ChangeState(movementStates.dead);
    }

    public void ChangeState(movementStates targetState)
    {
        if (currentState != movementStates.dead)
        {
            currentState = targetState;
        }
    }

    public bool IsDead()
    {
        return dead;
    }

    protected virtual void StateLogic()
    {
        switch (currentState)
        {
            case movementStates.moving:
                if (!dead)
                {
                    CheckForSpriteFlip();
                    agent.SetDestination(playerTarget.position);
                }
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
