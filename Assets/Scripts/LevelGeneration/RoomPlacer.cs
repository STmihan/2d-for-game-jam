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
    public int curRoom;

    public List<Room> spawnedRooms = new List<Room>();

    private void Start()
    {
        spawnedRooms.Add(startRoom);
        curRoom = 0;
    }

    private void Update()
    {
        curRoom = spawnedRooms.Count - 1;
        if (player.position.y < rooms[curRoom].Forward.position.y - 20) SpawnRoom();
    }

    private void SpawnRoom()
    {
        Room newRoom = Instantiate(rooms[Random.Range(0, rooms.Length)]);
        newRoom.transform.position = spawnedRooms[spawnedRooms.Count-1].Forward.position - newRoom.Back.localPosition;
        spawnedRooms.Add(newRoom);

        if (spawnedRooms.Count > 3)
        {
            Destroy(spawnedRooms[0].gameObject);
            spawnedRooms.RemoveAt(0);
        }
    }
}
