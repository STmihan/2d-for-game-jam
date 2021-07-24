using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float bulletSpeed = 50f;
    private Enemy _enemy;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddRelativeForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, 2f);
        _enemy = gameObject.AddComponent<Enemy>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.collider.CompareTag($"Player") && !other.collider.CompareTag($"Bullet"))
            Destroy(gameObject);
        if (other.collider.CompareTag($"Player") && !other.collider.CompareTag($"Bullet"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(_enemy.damage);
            Destroy(gameObject);
        }
    }
}