using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemSpawners;

    [SerializeField] private List<GameObject> spawners;
    [SerializeField] private List<GameObject> shuffledSpawners;
    [SerializeField] private int minEnemies;
    [SerializeField] private int maxEnemies;

    [SerializeField] private int spawnedEnemies;
    [SerializeField] private GameObject roomHandler;
    [SerializeField] private GameObject itemSpawner;
    private int enemiesRemaining;

    void Start()
    {
        spawnedEnemies = Random.Range(minEnemies, maxEnemies);

        enemiesRemaining = spawnedEnemies;
        GetSpawners();

        shuffledSpawners = GetRandomSpawnpoints();
    }

    void Update()
    {
        
    }

    public GameObject GetItemSpawner()
    {
        return itemSpawner;
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < spawnedEnemies; i++)
        {
            shuffledSpawners[i].GetComponent<EnemySpawnPoint>().SpawnEnemy(transform);
        }
    }

    private void GetSpawners()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("EnemySpawnPoint"))
            {
                spawners.Add(transform.GetChild(i).gameObject);
            }
        }
    }

    public void EnemyKilled()
    {
        enemiesRemaining--;
        if (enemiesRemaining == 0)
        {
            roomHandler.GetComponent<DungeonRoomManager>().OpenDoors();
            itemSpawner.GetComponent<ItemSpawner>().SpawnItems();
        }
    }

    private List<GameObject> GetRandomSpawnpoints()
    {
        System.Random rand = new System.Random();
        List<GameObject> randomSpawners = spawners.OrderBy(x => rand.Next()).ToList();
        return randomSpawners;
    }
}
