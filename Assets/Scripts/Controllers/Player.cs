using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    #region Damage, Speed, HP
    [Header("Damage, Speed, HP")]
    [SerializeField]public int damage = 20;
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
    #endregion

    public float _curGold;
    public bool _isInBattle;
    
    #region States
    private enum HPState
    {
        FullHP,
        HalfHP,
        Death
    }
    private HPState HpState = HPState.FullHP;
    [SerializeField] private Sprite[] playerStates = new Sprite[3];
    #endregion
    
    [SerializeField] private AudioSource audioSource;
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
        HPStateChange(HpState);
        StepSoundChange(_isInBattle);
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
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, angle), 100f * Time.deltaTime);
    }

    private void GoldTimer()
    {
        _curGold -= Time.deltaTime * Time.time;
    }
    #endregion

    #region Public methods
    public void TakeDamage(float damage)
    {
        _curGold -= damage;
        HitEffect();
    }

    public void HealOn()
    {
        _curGold += 10f;
        if (_curGold > MaxGold)
            _curGold = MaxGold;
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

    #region audio

    void AudioMove(float vol)
    {
        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && !audioSource.isPlaying)
        {
            audioSource.volume = vol;
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.loop = true;
            audioSource.PlayOneShot(audioSource.clip);
        }
        else if ((!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))&& audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    
    void StepSoundChange(bool isInBat)
    {
        if (isInBat)
        {
            AudioMove(0.3f);
        }
        else
        {
            AudioMove(.7f);
        }
    }
    #endregion

    #region StateChanges
    private void HPStateChange(HPState state)
    {
        if (_curGold > 50)
            state = HPState.FullHP;
        if (_curGold <= 50)
            state = HPState.HalfHP;
        if (_curGold <= 0)
        {
            state = HPState.Death;
            Time.timeScale = 0;
        }

        switch (state)
        {
            case HPState.FullHP:
                _spriteRenderer.sprite = playerStates[0];
                break;
            case HPState.HalfHP:
                _spriteRenderer.sprite = playerStates[1];
                break;
            case HPState.Death:
                _spriteRenderer.sprite = playerStates[2];
                _spriteRenderer.material = _origMaterial;
                audioSource.gameObject.SetActive(false);
                break;
        }
    }

    #endregion
}