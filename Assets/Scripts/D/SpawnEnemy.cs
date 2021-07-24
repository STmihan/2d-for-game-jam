using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    Transform spawnPoint;
    BoxCollider2D box2D;
    public GameObject[] typeEnemy;

    [Header("Волны врагов:")]    
    public int curentWave;
    public int maxWave;



    void Start()
    {
        spawnPoint = transform;
        box2D = GetComponent<BoxCollider2D>();
        StartCoroutine(Spawn(1));
    }
    IEnumerator Spawn(int enemyCount)
    {
        for (int i=0; i < enemyCount; i++)
            {
                GameObject newEnemy = Instantiate(typeEnemy[Random.Range(0, typeEnemy.Length)]) as GameObject;
                newEnemy.transform.position = new Vector3(Random.Range(box2D.bounds.min.x, box2D.bounds.max.x),  Random.Range(box2D.bounds.min.y, box2D.bounds.max.y), 
                    spawnPoint.transform.position.z);               
            }
        curentWave++;
        yield return new WaitForSeconds(4f);
        if (curentWave < maxWave) StartCoroutine(Spawn(enemyCount + 1));
     


    }
    

}
