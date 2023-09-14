using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePickup : MonoBehaviour
{
    [SerializeField] private List<GameObject> gunList = new List<GameObject>();

    // List of possible gun drops in crate
    [SerializeField] private List<GameObject> gunDropList = new List<GameObject>();
    [SerializeField] private SpriteRenderer spriteRen;

    // Gun prefab to pass
    protected GameObject containedGun;

    // Pickup contained in crate
    protected GameObject containedPickup;
    protected GameObject playerRef;

    private int listSize;
    public bool playerColliding = false;

    void Start()
    {
        listSize = gunList.Count;

        // Get a random gun to be contained in the crate - used for WeaponPickup inheritance
        containedGun = GetContainedGun();

        // Get a random pickup item to drop when the crate is opened
        containedPickup = GetDropContained();
    }

    void Update()
    {
        CheckForSpawnPickup();
    }

    // Randomly select a gun from the gun list
    private GameObject GetContainedGun()
    {
        int randomIndex = Random.Range(0, listSize);
        
        return gunList[randomIndex];
    }

    // Randomly select a pickup item from the drop list
    private GameObject GetDropContained()
    {
        int randomIndex = Random.Range(0, listSize);

        return gunDropList[randomIndex];
    }

    protected void PlaySoundEffect()
    {
        playerRef = GameObject.FindWithTag("Player");
        playerRef.GetComponent<PlayerSFXManager>().PlayPickupSFX();
    }

    // Spawn the contained pickup item at the crate's position and destroy the crate
    private void CheckForSpawnPickup()
    {
        if (playerColliding && Input.GetKeyDown(KeyCode.E))
        {
            GameObject gunPickup = Instantiate(containedPickup, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerColliding = false;
        }
    }
}
