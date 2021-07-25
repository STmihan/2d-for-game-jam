using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private bool isEnemyBullet;
    
    [SerializeField] private GameObject rangeEnemy;
    [SerializeField] private GameObject player;
    
    private Rigidbody2D _rigidbody2D;
    private Enemy _enemy;
    private Player _player;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddRelativeForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, 1);
        _enemy = rangeEnemy.GetComponent<Enemy>();
        _player = player.GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var gm = other.gameObject;
        var col = other.collider;
        
        if (isEnemyBullet)
        {
            if (col.CompareTag($"Player"))
            {
                gm.GetComponent<Player>().TakeDamage(_enemy.damage);
            }
        }
        else
        {
            if (col.CompareTag($"Enemy") && gm.GetComponent<Enemy>())
            {
                gm.GetComponent<Enemy>().TakeDamage(_player.damage);
            }
        }
        if (!col.CompareTag($"Bullet"))
        {
            Destroy(gameObject);
        }
    }
}