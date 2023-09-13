using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemSpawnPoints;
    [SerializeField] private List<GameObject> shuffledSpawnPoints;
    [SerializeField] private int maxSpawnPoints = 2;
    [SerializeField] private int actualSpawnPoints;
    void Start()
    {
        actualSpawnPoints = Random.Range(0, maxSpawnPoints + 1);
    }

    private List<GameObject> GetRandomSpawnpoints()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            itemSpawnPoints.Add(transform.GetChild(i).gameObject);
        }

        System.Random rand = new System.Random();
        List<GameObject> randomItemSpawnPoints = itemSpawnPoints.OrderBy(x => rand.Next()).ToList();
        return randomItemSpawnPoints;
    }

    public void SpawnItems()
    {
        shuffledSpawnPoints = GetRandomSpawnpoints();
        for (int i = 0; i < maxSpawnPoints; i++)
        {
            shuffledSpawnPoints[i].GetComponent<ItemSpawnPoint>().SignalSpawnPoint();
        }
    }
}
