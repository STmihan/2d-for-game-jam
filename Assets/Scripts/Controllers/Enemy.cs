﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region HP, damage and Speed fields
    [Header("HP, damage and Speed fields")]
    public float damage = 2;
    [SerializeField] private int maxHP = 60;
    [SerializeField] private float speed = 2;
    
    [SerializeField] private float attackRange = 2;
    [SerializeField] private float attackDelay = 2;
    #endregion
    
    [Space]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask pLayerMask;
    [SerializeField] private bool isRange;

    #region HitEffect
    [Header("HitEffect")]
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;
    
    private SpriteRenderer _spriteRenderer;
    private Material _origMaterial;
    
    private Coroutine _flashCoroutine;
    #endregion
    #region Private fields
    private Transform _target;
    private Player _targetComponent;
    private Rigidbody2D _rigidbody2D;
    public GameObject Manager;

    private float _nextAttackTime;
    private int _curHP;
    private GameManager _manager;
    #endregion

    public bool isInBattle;
    
    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _origMaterial = _spriteRenderer.material;
        _target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _targetComponent = _target.GetComponent<Player>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _curHP = maxHP;
    }

    #region Updates Methods
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
    #endregion
    #region Movement
    private void FollowTarget()
    {
        Rotate();
        if (Vector2.Distance(transform.position, _target.position) > attackRange)
        {
            Move();
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
    #endregion
    #region Attack Methods
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
    #endregion

    #region HitEffect
    private void HitEffect()
    {
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        _flashCoroutine = StartCoroutine(FlashRoutine());
    }
    
    private IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        _spriteRenderer.material = _origMaterial;

        _flashCoroutine = null;
    }

    #endregion
    
    #region Public methods
    public void TakeDamage(int dm)
    {
        _curHP -= dm;
        HitEffect();
        if (_curHP <= 0)
        {
            Destroy(gameObject);
            _targetComponent.HealOn();
            GameObject.FindWithTag("Manager").GetComponent<GameManager>().score++;
        }
    }
    #endregion
}
