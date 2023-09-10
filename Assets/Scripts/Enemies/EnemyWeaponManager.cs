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

    [SerializeField] private float cooldown;
    [SerializeField] private bool playerDetected = false;
    [SerializeField] private bool reloading = false;

    private GunShooting shootingScript;
    void Start()
    {
        parentMover = currentParent.GetComponent<ShootingEnemyMovement>();

        chosenGun = ChooseGun();

        SpawnGun();


        shootingScript = currentGun.GetComponent<GunShooting>();
        //cooldown = shootingScript.cooldown;
        //shootingScript.ownedByPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (parentMover.dead)
        {
            currentGun.SetActive(false);
        }

        CheckForShooting();
    }

    private void SpawnGun()
    {
        currentGun = Instantiate(chosenGun, transform.position, Quaternion.identity);
        currentGun.transform.parent = transform;
        currentGun.GetComponent<Gun>().ownedByPlayer = false;
    }

    private GameObject ChooseGun()
    {
        return availableGuns[Random.Range(0, availableGuns.Length)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            parentMover.ChangeState(EnemyMovement.movementStates.attacking);
            //shootingScript.Shoot();
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

    private void CheckForShooting()
    {
        if (!reloading && playerDetected)
        {
            //shootingScript.Shoot();
            reloading = true;
            Invoke("Reload", cooldown);
        }
    }

    private void Reload()
    {
        reloading = false;
    }
}
