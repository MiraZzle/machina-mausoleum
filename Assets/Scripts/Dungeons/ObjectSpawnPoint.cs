using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] roomObjects;
    [SerializeField] private float spawnChance = 50f;
    public bool elevatorSpawner = false;

    private float upperChanceBound = 100f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseObject()
    {
        float randomSpawnChance = Random.Range(0, upperChanceBound);

        if (randomSpawnChance < spawnChance)
        {
            GameObject objectToSpawn = roomObjects[Random.Range(0, roomObjects.Length)];
            GameObject objectInstance = Instantiate(objectToSpawn, gameObject.transform.position, Quaternion.identity);
        }
    }

    public void SpawnElevator(bool etrance)
    {
        GameObject objectToSpawn = roomObjects[0];
        GameObject objectInstance = Instantiate(objectToSpawn, gameObject.transform.position, Quaternion.identity);
        objectInstance.GetComponent<ElevatorManager>().SetState(etrance);
    }
}
