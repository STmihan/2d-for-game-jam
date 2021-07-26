using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private bool isEnemyBullet;
    [SerializeField] private bool isBigHit;
    [SerializeField] private LayerMask _layer;
    
    [SerializeField] private GameObject rangeEnemy;
    [SerializeField] private GameObject player;
    [SerializeField] private float radius;

    [Header("Hit Effects")] [SerializeField]
    private GameObject commonHitEffect;
    
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
            if (!col.CompareTag($"Bullet"))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (col.CompareTag($"Enemy") && gm.GetComponent<Enemy>())
            {
                Collider2D[] hit = Physics2D.OverlapCircleAll(other.contacts[0].point, radius,_layer);
                
                foreach (var VARIABLE in hit)
                {
                     VARIABLE.GetComponent<Enemy>().TakeDamage(_player.damage);
                }
            }
            if (!col.CompareTag($"Bullet"))
            {
                Destroy(gameObject);
            }
            HitEffect(other);
        }
    }

    private void HitEffect(Collision2D other)
    {
        List<ContactPoint2D> contact = new List<ContactPoint2D>();
        other.GetContacts(contact);
        var comhiteff = Instantiate(commonHitEffect, contact[0].point, quaternion.identity);
        Destroy(comhiteff, .6f);
    }
}