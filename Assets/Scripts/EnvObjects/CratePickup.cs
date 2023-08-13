using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePickup : MonoBehaviour
{
    protected GameObject containedGun;

    [SerializeField] private List<GameObject> gunList = new List<GameObject>();

    private int listSize;

    public bool playerColliding = false;

    void Start()
    {
        listSize = gunList.Count;

        containedGun = GetContainedGun();
    }

    void Update()
    {
        CheckForPickup();
    }

    private GameObject GetContainedGun()
    {
        int randomIndex = Random.Range(0, listSize);
        
        return gunList[randomIndex];
    }

    protected void CheckForPickup()
    {
        if (playerColliding && Input.GetKeyDown(KeyCode.E))
        {
            GameObject gunManager = GameObject.FindGameObjectWithTag("WeaponManager");
            gunManager.GetComponent<PlayerWeaponManager>().AddGun(containedGun);

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
