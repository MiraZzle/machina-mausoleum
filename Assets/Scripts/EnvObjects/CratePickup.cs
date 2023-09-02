using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePickup : MonoBehaviour
{
    protected GameObject containedGun;
    protected GameObject containedPickup;

    [SerializeField] private SpriteRenderer spriteRen;

    [SerializeField] private Sprite unActiveS;
    [SerializeField] private Sprite ActiveS;

    [SerializeField] private List<GameObject> gunList = new List<GameObject>();
    [SerializeField] private List<GameObject> gunDropList = new List<GameObject>();

    private int listSize;

    public bool playerColliding = false;

    void Start()
    {
        //spriteRen.sprite = unActiveS;
        listSize = gunList.Count;

        containedGun = GetContainedGun();
        containedPickup = GetDropContained();
    }

    void Update()
    {
        CheckForSpawnPickup();
    }

    private GameObject GetContainedGun()
    {
        int randomIndex = Random.Range(0, listSize);
        
        return gunList[randomIndex];
    }

    private GameObject GetDropContained()
    {
        int randomIndex = Random.Range(0, listSize);

        return gunDropList[randomIndex];
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
            //spriteRen.sprite = ActiveS;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerColliding = false;
            //spriteRen.sprite = unActiveS;
        }
    }
}
