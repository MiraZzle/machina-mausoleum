using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyMovement : EnemyMovement
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        StateLogic();
    }

    protected override void StateLogic()
    {
        switch (currentState)
        {
            case movementStates.moving:
                if (!dead)
                {
                    agent.SetDestination(playerTarget.position);
                    CheckForSpriteFlip();
                    animator.SetBool("Moving", true);
                }
                break;
            case movementStates.guarding:
                break;
            case movementStates.attacking:
                animator.SetBool("Moving", false);
                break;
            case movementStates.dead:
                weaponManager.GetComponent<EnemyWeaponManager>().DisableGun();
                agent.enabled = false;
                break;
        }
    }
}
