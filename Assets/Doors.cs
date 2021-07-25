using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public GameObject Forward;

    public GameObject[] enemies;
    private bool trigger;

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            Forward.SetActive(false);
        }
        else
        {
            Forward.SetActive(true);
        }
    }
}
