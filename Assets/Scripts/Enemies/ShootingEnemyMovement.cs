using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyMovement : EnemyMovement
{
    void Update()
    {
        StateLogic();
    }

    // Override the StateLogic method from the base class
    protected override void StateLogic()
    {
        switch (currentState)
        {
            case movementStates.moving:
                if (!dead)
                {
                    agent.enabled = true;
                    agent.SetDestination(playerTarget.position);
                    CheckForSpriteFlip();
                    animator.SetBool("Moving", true);
                }
                break;
            case movementStates.guarding:
                break;
            case movementStates.attacking:
                CheckForSpriteFlipIdle();
                agent.enabled = false;
                animator.SetBool("Moving", false);
                break;
            case movementStates.dead:
                weaponManager.GetComponent<EnemyWeaponManager>().DisableGun();
                agent.enabled = false;
                break;
        }
    }
}
