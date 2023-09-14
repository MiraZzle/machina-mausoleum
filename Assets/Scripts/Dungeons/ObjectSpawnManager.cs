using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject roomHandler;
    [SerializeField] private Transform[] objectPositions;
    [SerializeField] private Transform[] artificialWallPos;

    // Transform for elevator spawn position
    [SerializeField] private Transform elevatorPos;
    [SerializeField] private Transform keySpawnPos;

    // Spawn objects at each object spawn position - currently used for single spawnpoint
    public void SpawnObjects()
    {
        foreach (Transform t in objectPositions)
        {
            ObjectSpawnPoint go = t.GetComponent<ObjectSpawnPoint>();
            go.ChooseObject();
        }
    }

    // Spawn an elevator based on entrance and exit conditions - determine if elevator should be closed or opened
    public void SpawnElevator(bool exit, bool entrance)
    {
        ObjectSpawnPoint elevatorSpawner = elevatorPos.GetComponent<ObjectSpawnPoint>();

        elevatorPos.GetComponent<ObjectSpawnPoint>().elevatorSpawner = true;
        elevatorPos.GetComponent<ObjectSpawnPoint>().SpawnElevator(entrance);
    }

    // Spawn structure holding key to elevator
    public void SpawnKeyStructure()
    {
        keySpawnPos.GetComponent<ObjectSpawnPoint>().SpawnKeyPosition();
    }
}
