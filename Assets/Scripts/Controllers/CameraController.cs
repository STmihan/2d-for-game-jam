﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smooth = 2f;
    void FixedUpdate()
    {
        Vector3 targetPos = target.position;
        targetPos.z = -10f;
        
        transform.position = Vector3.Lerp(transform.position, targetPos, smooth * Time.fixedDeltaTime);
    }
}