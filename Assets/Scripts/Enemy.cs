using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 2;

    [SerializeField] private int maxHP = 60;
    [SerializeField] private float speed = 2;
    
    [SerializeField] private float attackRange = 2;
    [SerializeField] private float attackDelay = 2;

    [SerializeField] private GameObject bullet;
    
    [SerializeField] private bool isRange;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask pLayerMask;

    private Transform _target;
    private Player _targetComponent;
    private Rigidbody2D _rigidbody2D;

    private float _nextAttackTime;
    private int _curHP;

    private void Start()
    {
        _target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _targetComponent = _target.GetComponent<Player>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _curHP = maxHP;
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _target.position) <= attackRange)
        {
            Attack();
        }
    }

    private void FollowTarget()
    {
        Rotate();
        if (Vector2.Distance(transform.position, _target.position) > attackRange)
        {
            Move();
        }
    }

    private void Attack()
    {
        if (Time.time > _nextAttackTime)
        {
            if (!isRange)
            {
                MeeleAttack();
            }
            else
            {
                RangeAttack();
            }
            _nextAttackTime = Time.time + attackDelay;
        }
    }

    private void Move()
    {
        Vector2 dir =  _target.position - transform.position;
        _rigidbody2D.MovePosition(_rigidbody2D.position + dir.normalized * (speed * Time.fixedDeltaTime));
    }

    private void Rotate()
    {
        Vector2 dir =  _target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void MeeleAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, pLayerMask);

        if (hit.gameObject.GetComponent<Player>())
            _targetComponent.TakeDamage(damage);
    }

    private void RangeAttack()
    {
        Instantiate(bullet, attackPoint.position, attackPoint.rotation);
    }

    public void TakeDamage(int dm)
    {
        _curHP -= dm;
        if (_curHP <= 0)
        {
            Destroy(gameObject);
            _targetComponent.HealOn();
        }
    }
}
