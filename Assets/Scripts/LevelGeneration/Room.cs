using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    [Header("Пол")]
    [SerializeField] private Tilemap tilemapBase;
    [SerializeField] private Tile[] tiles;

    [Header("Камни")]
    [SerializeField] private GameObject StoneParent;
    [SerializeField] private Sprite[] stones;
    [SerializeField] private Sprite stoneFill;

    [Header("Стены")] 
    [SerializeField] private Tilemap tilemapRocks;
    [SerializeField] private Tile Rock;

    [Space]
    public Transform Forward;
    public Transform Back;
    
    private void Start()
    {
        for (int x = -100; x < 100; x++)
        {
            for (int y = -100; y < 100; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (tilemapBase.HasTile(tilePos))
                {
                    var tile = tiles[Random.Range(0, tiles.Length)];
                    tilemapBase.SetTile(tilePos, tile);
                }
            }
        }

        for (int x = -50; x < 50; x++)
        {
            for (int y = -50; y < 50; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (!tilemapBase.HasTile(tilePos))
                {
                    tilemapRocks.SetTile(tilePos, Rock);
                }
            }
        }
        
        foreach (SpriteRenderer filter in StoneParent.GetComponentsInChildren<SpriteRenderer>())
        {
            float random = Random.Range(0, 100);
                if (random <= 40)
                {
                    filter.sprite = stones[Random.Range(0, stones.Length)];
                    filter.gameObject.transform.rotation = quaternion.Euler(0, 0, Random.Range(0, 3) * 90);
                }

                if (filter.sprite == stoneFill)
                    filter.sprite = null;
        }
    }
}