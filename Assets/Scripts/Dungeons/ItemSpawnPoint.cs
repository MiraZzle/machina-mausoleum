using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    // The chosen item prefab to spawn
    [SerializeField] private GameObject chosenItem;

    // The percentage chance to spawn an item
    [SerializeField] private int spawnPercentage = 60;

    // Signal this item spawn point to potentially spawn an item
    public void SignalSpawnPoint()
    {
        chosenItem = ChooseItem();
        TestSpawn();
    }

    // Test if the item should spawn based on spawnPercentage
    private void TestSpawn()
    {
        if (Random.Range(0, 100) < spawnPercentage)
        {
            SpawnItem();
        }
    }

    // Choose a random item from the array of items
    private GameObject ChooseItem()
    {
        return items[Random.Range(0, items.Length)];
    }

    private void SpawnItem()
    {
        GameObject itemInstance = Instantiate(chosenItem, transform.position, Quaternion.identity);
    }
}
