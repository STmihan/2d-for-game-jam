using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private GameObject RangeEnemy;
    private Enemy _enemy;
    private float _damage;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddRelativeForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, 2f);
        _enemy = RangeEnemy.GetComponent<Enemy>();
        _damage = _enemy.damage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.collider.CompareTag($"Player") && !other.collider.CompareTag($"Bullet"))
            Destroy(gameObject);
        if (other.collider.CompareTag($"Player") && !other.collider.CompareTag($"Bullet"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(_damage);
                Destroy(gameObject);
        }
    }
}