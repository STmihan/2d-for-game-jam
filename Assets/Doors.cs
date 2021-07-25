using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private GameObject Forward;
    private GameObject Arrow;
    private GameObject Player;


    private void Start()
    {
        Arrow = GameObject.FindWithTag("Arrow");
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0 || Vector2.Distance(enemies[0].transform.position, Player.transform.position) > 5)
            Arrow.SetActive(true);
        else 
            Arrow.SetActive(false);
        
        if (enemies.Length == 0)
            Forward.SetActive(false);
        else
            Forward.SetActive(true);
    }
}
