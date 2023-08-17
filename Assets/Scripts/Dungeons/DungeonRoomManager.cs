using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonRoomManager : MonoBehaviour
{
    private int availableSides = 4;

    [SerializeField] private bool isSpawnRoom = false;
    public bool roomEntered = false;
    public bool roomCleaned = false;

    [SerializeField] GameObject[] doors;

    [SerializeField] private GameObject[] entrances = new GameObject[4];
    [SerializeField] private GameObject[] blocks = new GameObject[4];

    // side order = {top, right, bottom, left}
    [SerializeField] private bool[] activeSides = new bool[4];

    void Start()
    {
        //doors = GameObject.FindGameObjectsWithTag("door");
    }

    void Update()
    {
        
    }
    private void HandleDoors()
    {

    }

    public void SetActiveEntrances(bool[] sidesToActivate)
    {
        for (int i = 0; i < sidesToActivate.Length; i++)
        {
            activeSides[i] = sidesToActivate[i];

            entrances[i].SetActive(sidesToActivate[i]);
            blocks[i].SetActive(!sidesToActivate[i]);
        }
    }
}
