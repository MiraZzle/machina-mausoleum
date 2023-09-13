using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] availableGuns;
    private GameObject chosenGun;
    [SerializeField] private GameObject currentGun;

    [SerializeField] private GameObject currentParent;
    private ShootingEnemyMovement parentMover;

    [SerializeField] private CircleCollider2D detectionRadius;
    [SerializeField] private float cooldown;
    [SerializeField] private bool playerDetected = false;
    [SerializeField] private bool reloading = false;

    private GunShooting shootingScript;
    void Start()
    {
        detectionRadius = GetComponent<CircleCollider2D>();
        parentMover = currentParent.GetComponent<ShootingEnemyMovement>();

        chosenGun = ChooseGun();

        SpawnGun();


        shootingScript = currentGun.GetComponent<GunShooting>();
        cooldown = shootingScript.GetCooldown();
        SetRadius();
    }

    void Update()
    {
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
        if (!reloading && playerDetected)
        {
            shootingScript.EnemyShooting();
            StartCoroutine(StartCooldown());
        }
    }
}
