using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject chosenItem;
    [SerializeField] private int spawnPercentage = 60;
    [SerializeField] private bool willSpawn;

    public void SignalSpawnPoint()
    {
        chosenItem = ChooseItem();
        TestSpawn();

    }

    private void TestSpawn()
    {
        if (Random.Range(0, 100) < spawnPercentage)
        {
            SpawnItem();
        }
    }

    private GameObject ChooseItem()
    {
        return items[Random.Range(0, items.Length)];
    }

    private void SpawnItem()
    {
        GameObject itemInstance = Instantiate(chosenItem, transform.position, Quaternion.identity);
    }


}
