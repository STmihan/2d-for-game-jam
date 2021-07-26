using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private GameObject Forward;
    private GameObject _arrow;
    private AudioSource _ao;
    private Player _player;

    public GameObject[] Enemies;
    private void Start()
    {
        _ao = GameObject.FindWithTag("AudioMain").GetComponent<AudioSource>();
        _arrow = GameObject.FindWithTag("Arrow");
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (Enemies.Length == 0)
        {
            Forward.SetActive(false);
            _arrow.SetActive(true);
            BackgroundMusicChange(0.2f);
            _player._isInBattle = false;
        }
        else
        {
            BackgroundMusicChange(.7f);
            Forward.SetActive(true);
            _arrow.SetActive(false);
            _player._isInBattle = true;
        }
    }
    
    void BackgroundMusicChange(float vol)
    {
        _ao.volume = vol;
    }
}
