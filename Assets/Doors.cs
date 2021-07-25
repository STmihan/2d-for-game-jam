using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private GameObject Forward;
    private GameObject Arrow;
    
    private void Start()
    {
        Arrow = GameObject.FindWithTag("Arrow");
    }

    void Update()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Forward.SetActive(false);
            Arrow.SetActive(true);
        }
        else
        {
            Forward.SetActive(true);
            Arrow.SetActive(false);
        }
    }
}
