using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] objectPositions;
    [SerializeField] private GameObject roomHandler;

    [SerializeField] private Transform[] artificialWallPos;
    [SerializeField] private Transform elevatorPos;
    [SerializeField] private Transform keySpawnPos;


    void Start()
    {
        

    }

    void Update()
    {
        
    }

    public void SpawnObjects()
    {
        foreach (Transform t in objectPositions)
        {
            ObjectSpawnPoint go = t.GetComponent<ObjectSpawnPoint>();
            go.ChooseObject();
        }
    }

    public void SpawnElevator(bool exit, bool entrance)
    {
        ObjectSpawnPoint elevatorSpawner = elevatorPos.GetComponent<ObjectSpawnPoint>();
        //elevatorSpawner.elevatorSpawner = true;
        elevatorPos.GetComponent<ObjectSpawnPoint>().elevatorSpawner = true;
        elevatorPos.GetComponent<ObjectSpawnPoint>().SpawnElevator(entrance);
    }

    public void SpawnKeyStructure()
    {
        keySpawnPos.GetComponent<ObjectSpawnPoint>().SpawnKeyPosition();
    }
}
