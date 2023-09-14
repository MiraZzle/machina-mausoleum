using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AxeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject axePrefab;

    // List to store spawned axes
    [SerializeField] private List<GameObject> axeList = new List<GameObject>();
    [SerializeField] private int axeCount = 4;

    // Full rotation in degrees
    [SerializeField] private int fullRotationDeg = 360;
    void Start()
    {
        SpawnAxes();
    }

    // Spawn axe and calculate rotational differences between each of them
    private void SpawnAxes()
    {
        // Calculate the rotational difference between each axe
        float rotationalDifference = fullRotationDeg / axeCount;
        for (int i = 0; i < axeCount; i++)
        {
            GameObject axe = Instantiate(axePrefab, transform.position, Quaternion.identity);
            axe.transform.parent = transform;
            axe.transform.rotation = Quaternion.Euler(0, 0, rotationalDifference * i);
            axeList.Add(axe);
        }
    }

    public void DisableAxes()
    {
        foreach (GameObject axe in axeList)
        {
            axe.SetActive(false);
        }
    }
}
