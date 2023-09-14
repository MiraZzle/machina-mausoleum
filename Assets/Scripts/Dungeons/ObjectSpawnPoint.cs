using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnPoint : MonoBehaviour
{
    // Array of objects that can be spawned
    [SerializeField] private GameObject[] roomObjects;

    // Percentage (chance) of spawning an object
    [SerializeField] private float spawnChance = 50f;

    private float upperChanceBound = 100f;
    public bool elevatorSpawner = false;

    // Choose and potentially spawn an object based on the spawn chance
    public void ChooseObject()
    {
        float randomSpawnChance = Random.Range(0, upperChanceBound);

        if (randomSpawnChance < spawnChance)
        {
            GameObject objectToSpawn = roomObjects[Random.Range(0, roomObjects.Length)];
            GameObject objectInstance = Instantiate(objectToSpawn, gameObject.transform.position, Quaternion.identity);
        }
    }

    // Spawn an elevator object and set its state
    public void SpawnElevator(bool etrance)
    {
        // Assuming the first object is the elevator
        GameObject objectToSpawn = roomObjects[0];
        GameObject objectInstance = Instantiate(objectToSpawn, gameObject.transform.position, Quaternion.identity);
        objectInstance.GetComponent<ElevatorManager>().SetState(etrance);
    }

    public void SpawnKeyPosition()
    {
        // Assuming the first object is the key structure
        GameObject objectToSpawn = roomObjects[0];
        GameObject objectInstance = Instantiate(objectToSpawn, gameObject.transform.position, Quaternion.identity);
    }
}
