using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] availableGuns;
    [SerializeField] private GameObject currentGun;
    [SerializeField] private GameObject currentParent;

    // Radius for detecting player and triggering gun shot
    [SerializeField] private CircleCollider2D detectionRadius;

    // The cooldown time between shots
    [SerializeField] private float cooldown;
    [SerializeField] private bool playerDetected = false;
    [SerializeField] private bool reloading = true;

    // Time to prepare for shooting - used during start to not instantly shoot at player
    [SerializeField] private float prepareTime = 1f;

    private ShootingEnemyMovement parentMover;
    private GameObject chosenGun;
    private GunShooting shootingScript;
    void Start()
    {
        detectionRadius = GetComponent<CircleCollider2D>();
        parentMover = currentParent.GetComponent<ShootingEnemyMovement>();

        // Spawn and equip the chosen gun
        chosenGun = ChooseGun();
        SpawnGun();

        shootingScript = currentGun.GetComponent<GunShooting>();
        cooldown = shootingScript.GetCooldown();

        // Set the detection radius based on the gun's range
        SetRadius();

        StartCoroutine(ChargeOnStart());
    }

    void Update()
    {
        // Deactivate the gun when the parent (ranged enemy) is dead
        if (parentMover.IsDead())
        {
            currentGun.SetActive(false);
        }

        CheckForShooting();
    }

    private void SetRadius()
    {
        float radiusPassed = currentGun.GetComponent<GunShooting>().GetRadius();
        detectionRadius.radius = radiusPassed;
    }

    // Start the reloading process after being spawned
    IEnumerator ChargeOnStart()
    {
        reloading = true;

        yield return new WaitForSeconds(prepareTime);

        reloading = false;
    }

    private void SpawnGun()
    {
        currentGun = Instantiate(chosenGun, transform.position, Quaternion.identity);
        currentGun.transform.parent = transform;
        currentGun.GetComponent<Gun>().ownedByPlayer = false;
        currentGun.GetComponent<GunShooting>().SetOwner(false);
    }

    private GameObject ChooseGun()
    {
        return availableGuns[Random.Range(0, availableGuns.Length)];
    }

    public void DisableGun()
    {
        currentGun.SetActive(false);
        detectionRadius.enabled = false;
        reloading = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player detected, start attacking
            playerDetected = true;
            parentMover.ChangeState(EnemyMovement.movementStates.attacking);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            parentMover.ChangeState(EnemyMovement.movementStates.moving);
        }
    }

    IEnumerator StartCooldown()
    {
        reloading = true;
        yield return new WaitForSecondsRealtime(cooldown);

        reloading = false;
    }

    private void CheckForShooting()
    {
        if (!reloading && playerDetected && !GameStateManager.gamePaused)
        {
            shootingScript.EnemyShooting();
            StartCoroutine(StartCooldown());
        }
    }
}
