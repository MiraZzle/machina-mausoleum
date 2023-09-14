using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonRoomManager : MonoBehaviour
{
    [SerializeField] GameObject enemySpawner;
    [SerializeField] GameObject objectSpawner;
    [SerializeField] GameObject[] doors;

    [SerializeField] private GameObject[] entrances = new GameObject[4];

    // Array of blocked wall GameObjects in all directions - used for not linked side
    [SerializeField] private GameObject[] blocks = new GameObject[4];

    // Array to store which sides are active (top, right, bottom, left)
    [SerializeField] private bool[] activeSides = new bool[4];
    [SerializeField] private GameObject navmesh;

    // Flags to indicate room type and status
    public bool isExitRoom = false;
    public bool isSpawnRoom = false;
    public bool roomEntered = false;
    public bool roomCleaned = false;

    public enum roomTypes
    {
        normal,
        key,
        spawn,
        exit
    }

    public roomTypes roomType;

    void Start()
    {
        HandleDoors();
        SignalSpawner();
        navmesh.GetComponent<NavmeshBaker>().BakeNavmesh();
    }

    // Helper function for debugging - not intended for shipping
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

    public void OpenDoors()
    {
        roomCleaned = true;
        foreach (var door in doors)
        {
            door.GetComponent<DoorManager>().roomCleared = true;
        }
    }

    // Trigger room entry when a player enters
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (roomType == roomTypes.normal)
        {
            if (collision.CompareTag("Player") && !roomEntered)
            {
                foreach (var door in doors)
                {
                    roomEntered = true;
                    door.GetComponent<DoorManager>().roomEntered = true;
                }
                enemySpawner.GetComponent<EnemySpawner>().SpawnEnemies();
            }
        }
    }

    // Set active entrances and blocks based on the provided sides to activate
    public void SetActiveEntrances(bool[] sidesToActivate)
    {
        for (int i = 0; i < sidesToActivate.Length; i++)
        {
            activeSides[i] = sidesToActivate[i];
            entrances[i].SetActive(sidesToActivate[i]);
            blocks[i].SetActive(!sidesToActivate[i]);
        }
    }

    // Match and proceed with defined roomtype
    private void SignalSpawner()
    {
        ObjectSpawner spawnerRef = objectSpawner.GetComponent<ObjectSpawner>();

        switch (roomType)
        {
            case roomTypes.normal:
                spawnerRef.SpawnObjects();
                break;
            case roomTypes.key:
                spawnerRef.SpawnKeyStructure();
                break;
            case roomTypes.spawn:
                spawnerRef.SpawnElevator(isExitRoom, isSpawnRoom);
                break;
            case roomTypes.exit:
                spawnerRef.SpawnElevator(isExitRoom, isSpawnRoom);
                break;
        }
    }
}
