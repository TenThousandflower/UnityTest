using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform START;
    public float waitRate = 0.2f;
    public static int CountEnemyAlive = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            for (int j = 0; j < waves[i].count; j++)
            {
                CountEnemyAlive++;
                GameObject.Instantiate(waves[i].enemyPrefab, START.position, Quaternion.identity);
                if (j < waves[i].count)
                {
                    yield return new WaitForSeconds(waves[i].rate);
                }
            }

            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waitRate);
        }
    }
}