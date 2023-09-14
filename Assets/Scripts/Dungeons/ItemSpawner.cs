using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemSpawnPoints;

    // Shuffled list of item spawn points
    [SerializeField] private List<GameObject> shuffledSpawnPoints;
    [SerializeField] private int maxSpawnPoints = 3;

    // Number of item spawn points to activate
    [SerializeField] private int actualSpawnPoints;
    void Start()
    {
        actualSpawnPoints = Random.Range(0, maxSpawnPoints + 1);
    }

    // Get a list of item spawn points and in shuffled order
    private List<GameObject> GetRandomSpawnpoints()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            itemSpawnPoints.Add(transform.GetChild(i).gameObject);
        }

        System.Random rand = new System.Random();

        // Shuffle the list
        List<GameObject> randomItemSpawnPoints = itemSpawnPoints.OrderBy(x => rand.Next()).ToList();
        return randomItemSpawnPoints;
    }

    // Activate a random number of item spawn points
    public void SpawnItems()
    {
        shuffledSpawnPoints = GetRandomSpawnpoints();
        for (int i = 0; i < maxSpawnPoints; i++)
        {
            shuffledSpawnPoints[i].GetComponent<ItemSpawnPoint>().SignalSpawnPoint();
        }
    }
}
