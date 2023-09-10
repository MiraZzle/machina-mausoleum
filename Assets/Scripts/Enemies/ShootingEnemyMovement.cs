using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyMovement : EnemyMovement
{
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetupAgent();
        currentState = movementStates.moving;
    }

    void Update()
    {
        StateLogic();
    }

    public new void Die()
    {
        base.Die();
        currentState = movementStates.dead;
    }

    protected override void StateLogic()
    {
        switch (currentState)
        {
            case movementStates.moving:
                agent.SetDestination(playerTarget.position);
                CheckForSpriteFlip();
                animator.SetBool("Moving", true);
                break;
            case movementStates.guarding:
                break;
            case movementStates.attacking:
                animator.SetBool("Moving", false);
                break;
            case movementStates.dead:
                agent.enabled = false;
                break;
        }
    }
}
