﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    Transform spawnPoint;
    BoxCollider2D box2D;
    public GameObject[] typeEnemy;

    [Header("Волны врагов:")] 
    public int enemyInWawe = 1;
    public int curentWave;
    public int maxWave;
    public int spawnedEnemies;


    void Start()
    {
        spawnPoint = transform;
        box2D = GetComponent<BoxCollider2D>();
    }
    public IEnumerator Spawn(int enemyCount)
    {
        for (int i=0; i < enemyCount; i++)
            {
                GameObject newEnemy = Instantiate(typeEnemy[Random.Range(0, typeEnemy.Length)]) as GameObject;
                newEnemy.transform.position = new Vector3(Random.Range(box2D.bounds.min.x, box2D.bounds.max.x),  Random.Range(box2D.bounds.min.y, box2D.bounds.max.y), 
                    spawnPoint.transform.position.z);
                spawnedEnemies++;
            }
        curentWave++;
        yield return new WaitForSeconds(2f);
        if (curentWave < maxWave) StartCoroutine(Spawn(enemyCount + 1));
    }

    public void TriggerSpawn()
    {
        StartCoroutine(Spawn(enemyInWawe));
    }

}
