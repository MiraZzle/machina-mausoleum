using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawners;
    [SerializeField] private int minEnemies;
    [SerializeField] private int maxEnemies;

    [SerializeField] private int spawnedEnemies;
    [SerializeField] private GameObject roomHandler;
    private int enemiesRemaining;

    void Start()
    {
        spawnedEnemies = Random.Range(minEnemies, maxEnemies);
        enemiesRemaining = spawnedEnemies;
        GetSpawners();
    }

    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < spawnedEnemies; i++)
        {
            spawners[i].GetComponent<EnemySpawnPoint>().SpawnEnemy(transform);
        }
    }

    private void GetSpawners()
    {
        for(int i = 0; i < transform.childCount; i++)
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
        }
    }
}
