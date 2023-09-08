using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject[] enemyTypes;
    [SerializeField] GameObject chosenType;


    public void SpawnEnemy(Transform enemyParent)
    {
        chosenType = ChooseEnemy();
        GameObject enemyInst = Instantiate(chosenType, transform.position, Quaternion.identity);
        enemyInst.transform.parent = enemyParent;
    }

    private GameObject ChooseEnemy()
    {
        int enemyIndex = Random.Range(0, enemyTypes.Length);
        return enemyTypes[enemyIndex];
    }
}
