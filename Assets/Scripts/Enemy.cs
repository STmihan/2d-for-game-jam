using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage = 2;
    [SerializeField] private float speed = 2;
    [SerializeField] private float stopDistance = 2;

    private Transform _target;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var col = other.collider;
        if (other.collider.CompareTag($"Player"))
        {
            col.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private void FollowTarget()
    {
        if (Vector2.Distance(transform.position, _target.position) > stopDistance)
        {
            Move();
        }
        else
        {
            Attack();
        }
    }

    private void Attack()
    {
        
    }

    private void Move()
    {
        Vector2 dir =  _target.position - transform.position;
        _rigidbody2D.MovePosition(_rigidbody2D.position + dir.normalized * (speed * Time.fixedDeltaTime));
    }
}
