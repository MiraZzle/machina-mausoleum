using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    // Array of enemy prefabs to choose from
    [SerializeField] GameObject[] enemyTypes;

    // The chosen enemy prefab to instantiate
    [SerializeField] GameObject chosenType;

    // Spawn an enemy at this spawn point, with the given enemyParent as the parent
    public void SpawnEnemy(Transform enemyParent)
    {
        chosenType = ChooseEnemy();
        GameObject enemyInst = Instantiate(chosenType, transform.position, Quaternion.identity);
        enemyInst.transform.parent = enemyParent;
    }

    // Choose and get random enemy from the array of enemy types
    private GameObject ChooseEnemy()
    {
        int enemyIndex = Random.Range(0, enemyTypes.Length);
        return enemyTypes[enemyIndex];
    }
}
