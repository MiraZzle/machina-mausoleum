using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject roomHandler;
    [SerializeField] private GameObject itemSpawner;

    [SerializeField] private List<GameObject> itemSpawners;
    [SerializeField] private List<GameObject> spawners;

    // List of shuffled enemy spawn points for randomized positions
    [SerializeField] private List<GameObject> shuffledSpawners;

    [SerializeField] private int minEnemies;
    [SerializeField] private int maxEnemies;

    // Number of enemies to be spawned
    [SerializeField] private int spawnedEnemies;

    // Number of remaining enemies to clear room
    private int enemiesRemaining;
    private String spawnPointTag = "EnemySpawnPoint";

    void Start()
    {
        spawnedEnemies = Random.Range(minEnemies, maxEnemies);
        enemiesRemaining = spawnedEnemies;

        // Retrieve and shuffle the spawn points
        GetSpawners();
        shuffledSpawners = GetRandomSpawnpoints();
    }

    public GameObject GetItemSpawner()
    {
        return itemSpawner;
    }

    // Spawn enemies at shuffled spawn points
    public void SpawnEnemies()
    {
        for (int i = 0; i < spawnedEnemies; i++)
        {
            shuffledSpawners[i].GetComponent<EnemySpawnPoint>().SpawnEnemy(transform);
        }
    }

    // Get all children with "EnemySpawnPoint" tag and store them in the spawners list
    private void GetSpawners()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag(spawnPointTag))
            {
                spawners.Add(transform.GetChild(i).gameObject);
            }
        }
    }

    public void EnemyKilled()
    {
        enemiesRemaining--;

        // If all enemies are killed, open the doors and spawn items
        if (enemiesRemaining == 0)
        {
            roomHandler.GetComponent<DungeonRoomManager>().OpenDoors();
            itemSpawner.GetComponent<ItemSpawner>().SpawnItems();
        }
    }

    private List<GameObject> GetRandomSpawnpoints()
    {
        System.Random rand = new System.Random();

        // Shuffle the list of spawn points
        List<GameObject> randomSpawners = spawners.OrderBy(x => rand.Next()).ToList();
        return randomSpawners;
    }
}
