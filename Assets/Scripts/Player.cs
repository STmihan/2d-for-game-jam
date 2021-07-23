using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;
    private Rigidbody2D _rigidbody2D;
    private Camera _camera;
    [SerializeField] private float Gold = 60f;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
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
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        
        _rigidbody2D.AddForce(move * Speed);
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
        Gold -= Time.deltaTime;
    }
}