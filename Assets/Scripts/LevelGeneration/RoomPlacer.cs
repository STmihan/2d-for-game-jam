using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomPlacer : MonoBehaviour
{
    [SerializeField] private Room[] roomsPrefabs;
    [SerializeField] private Room startRoom;
    [SerializeField] private Transform grid;

    private Room[,] _spawnedRooms;
    
    private void Start()
    {
        _spawnedRooms = new Room[11, 11];
        _spawnedRooms[5, 5] = startRoom;

        for (int i = 0; i < 12; i++)
        {
            
        }
    }

    private void PlaceRoom()
    {
        
    }
}
