using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;
    private Rigidbody2D _rigidbody2D;
    private Camera _camera;
    [SerializeField] private float MaxGold = 60f;
    private float _curGold;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _curGold = MaxGold;
    }
    
    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        GoldTimer();
        
        Rotate();
    }

    void Move()
    {
        Vector2 move;
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        
        // _rigidbody2D.AddForce(move * (Speed * Time.fixedDeltaTime));
        _rigidbody2D.MovePosition(_rigidbody2D.position + move.normalized * (Speed * Time.fixedDeltaTime));
    }

    void Rotate()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePos - _rigidbody2D.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void GoldTimer()
    {
        _curGold -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        _curGold -= damage;
        if (_curGold <= 0) 
            this.gameObject.SetActive(false);
    }
}