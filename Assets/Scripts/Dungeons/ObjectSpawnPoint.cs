using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] roomObjects;
    [SerializeField] private float spawnChance = 50f;

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
}
