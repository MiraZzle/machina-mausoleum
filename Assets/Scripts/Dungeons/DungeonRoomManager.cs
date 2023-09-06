using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonRoomManager : MonoBehaviour
{
    private int availableSides = 4;

    public bool isExitRoom = false;
    public bool isSpawnRoom = false;

    public bool isKeyRoom = false;

    public bool roomEntered = false;
    public bool roomCleaned = false;

    [SerializeField] GameObject objectSpawner;
    [SerializeField] GameObject[] doors;

    [SerializeField] private GameObject[] entrances = new GameObject[4];
    [SerializeField] private GameObject[] blocks = new GameObject[4];

    // side order = {top, right, bottom, left}
    [SerializeField] private bool[] activeSides = new bool[4];

    void Start()
    {
        HandleDoors();
        SignalSpawner();

    }

    void Update()
    {
        HandleDoors();
    }
    private void HandleDoors()
    {

        if (Input.GetKeyDown(KeyCode.Backspace) && roomEntered)
        {
            roomCleaned = true;

            foreach (var door in doors)
            {
                door.GetComponent<DoorManager>().roomCleared = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSpawnRoom && !isExitRoom && !isKeyRoom)
        {
            if (collision.CompareTag("Player") && !roomEntered)
            {
                foreach (var door in doors)
                {
                    roomEntered = true;
                    door.GetComponent<DoorManager>().roomEntered = true;
                }
            }
        }
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

    private void SignalSpawner()
    {
        ObjectSpawner spawnerRef = objectSpawner.GetComponent<ObjectSpawner>();

        if (!isExitRoom && !isSpawnRoom && !isKeyRoom)
        {
            spawnerRef.SpawnObjects();
        }

        else 
        {
            if (isKeyRoom)
            {
                spawnerRef.SpawnKeyStructure();
            }
            else
            {
                spawnerRef.SpawnElevator(isExitRoom, isSpawnRoom);
            }
        }
    }
}
