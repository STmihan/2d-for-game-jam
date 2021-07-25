using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public GameObject Forward;

    public GameObject[] enemies;
    private bool trigger;

    private void Start()
    {
        trigger = Forward.GetComponent<BoxCollider2D>().enabled;
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            trigger = true;
        }
        else
        {
            trigger = false;
        }
    }
}
