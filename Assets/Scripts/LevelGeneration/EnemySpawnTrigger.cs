using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public Room Room;
    public SpawnEnemy[] SpawnEnemies;
    private bool en = true;
    private void OnTriggerEnter(Collider other)
    {
        if (!Room.isComplite)
        {
            foreach (var VARIABLE in SpawnEnemies)
            {
                StartCoroutine(VARIABLE.Spawn(VARIABLE.enemyInWawe));
            }
        }
    }
}
