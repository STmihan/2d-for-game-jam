using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Damage, Speed, HP
    [Header("Damage, Speed, HP")]
    public int damage = 20;
    
    [SerializeField] private float Speed;
    [SerializeField] public float MaxGold = 60f;
    
    #endregion

    #region HitEffect
    [Header("HitEffect")]
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;

    private SpriteRenderer _spriteRenderer;
    private Material _origMaterial;
    private Coroutine _flashCoroutine;

    #endregion

    #region Private fields
    private Rigidbody2D _rigidbody2D;
    private Camera _camera;
    
    public float _curGold;
    #endregion

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _curGold = MaxGold;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _origMaterial = _spriteRenderer.material;
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

    #region Move, Rotate and death methods
    private void Move()
    {
        Vector2 move;
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        _rigidbody2D.AddForce(move * Speed);
    }

    private void Rotate()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - _rigidbody2D.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void GoldTimer()
    {
        _curGold -= Time.deltaTime;
    }
    #endregion

    #region Public methods
    public void TakeDamage(float damage)
    {
        _curGold -= damage;
        HitEffect();
        if (_curGold <= 0) 
            this.gameObject.SetActive(false);
    }

    public void HealOn()
    {
        _curGold += 100f;
    }
    #endregion

    #region Hit Effect Method
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
}