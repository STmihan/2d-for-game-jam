using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomPlacer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Room[] rooms;
    [SerializeField] private Room startRoom;

    private List<Room> spawnedRooms = new List<Room>();

    private void Start()
    {
        spawnedRooms.Add(startRoom);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) SpawnRoom();
    }

    private void SpawnRoom()
    {
        Room newRoom = Instantiate(rooms[Random.Range(0, rooms.Length)]);
        newRoom.transform.position = spawnedRooms[spawnedRooms.Count-1].Forward.position - newRoom.Back.localPosition;
        spawnedRooms.Add(newRoom);
    }
}
