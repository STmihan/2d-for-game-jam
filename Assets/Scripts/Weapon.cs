using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform mozzle;
    [SerializeField] private float cooldownFire = 800;

    private bool isReady = true;
    private float nextFireTime = 0;

    private void Update()
    {
        Shoot();
        Reload();
    }

    private void Reload()
    {
        if (Time.time > nextFireTime)
        {
            isReady = true;
            nextFireTime = Time.time + 60/cooldownFire;
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && isReady)
        {
            Instantiate(bullet, mozzle.position, mozzle.rotation);
            isReady = false;
        }
    }
}
