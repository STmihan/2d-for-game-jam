using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float bulletSpeed = 50f;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddRelativeForce(new Vector2(0, 1) * bulletSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.collider.CompareTag($"Player") && !other.collider.CompareTag($"Bullet"))
            Destroy(gameObject);
    }
}