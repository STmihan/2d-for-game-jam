using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private RoomPlacer RoomPlacer; 

    // Update is called once per frame
    void Update()
    {
        var target = RoomPlacer.spawnedRooms[RoomPlacer.spawnedRooms.Count - 1].Forward.position;
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), 10);
    }
}
