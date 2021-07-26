using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public GameObject[] SpawnEnemies;
    private bool once = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (once)
        {
            foreach (var VARIABLE in SpawnEnemies)
            {
                VARIABLE.GetComponent<SpawnEnemy>().TriggerSpawn();
            }
            once = false;
        }
    }
}
