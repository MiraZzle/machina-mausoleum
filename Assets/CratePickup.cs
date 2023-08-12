using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePickup : MonoBehaviour
{
    private GameObject containedGun;

    [SerializeField] private GameObject gun1;
    [SerializeField] private GameObject gun2;
    [SerializeField] private GameObject gun3;
    [SerializeField] private GameObject gun4;
    [SerializeField] private GameObject gun5;
    [SerializeField] private GameObject gun6;

    List<GameObject> gunList = new List<GameObject>();

    private int listSize;

    public bool collided = false;

    void Start()
    {
        gunList.Add(gun1);
        gunList.Add(gun2);
        gunList.Add(gun3);
        gunList.Add(gun4);
        gunList.Add(gun5);
        gunList.Add(gun6);

        listSize = gunList.Count;
    }

    void Update()
    {
        containedGun = GetContainedGun();
    }

    private GameObject GetContainedGun()
    {
        int randomIndex = Random.Range(0, listSize);
        
        return gunList[randomIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            GameObject gunManager = GameObject.FindGameObjectWithTag("WeaponManager");
            gunManager.GetComponent<PlayerWeaponManager>().AddGun(containedGun);
        }
    }
}
