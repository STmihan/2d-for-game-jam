using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform bottom;

    [SerializeField] private Sprite[] blocks;
    
    [Space]
    public Transform Forward;
    public Transform Back;
    
    private void Start()
    {
        foreach (var filter in bottom.GetComponentsInChildren<SpriteRenderer>())
        {
            if (filter.sprite == blocks[0])
            {
                filter.sprite = blocks[Random.Range(0, blocks.Length)];
            }
        }
    }
}