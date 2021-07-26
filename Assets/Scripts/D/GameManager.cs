using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get { return _instance; }
    }
    static GameManager _instance = null;

    GameObject player;

    public bool isPause = false;

    [Header("Текущий результат:")]
    public int score = 0;
    public float timeScore = 0;
    [Header("Лучший результат:")]    
    public int hightScore;
    public float hightTimeScore;




    void Awake()
    {
        // if (_instance)
        // {
        //     // DestroyImmediate(gameObject);
        //     return;
        // }
        _instance = this;
        // DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        Time.timeScale = 1f;
    }
  
    private void Update()
    {
        timeScore += Time.deltaTime;
        if(PlayerPrefs.HasKey("HightScore") && PlayerPrefs.HasKey("HightTimeScore"))
        {
            hightScore = PlayerPrefs.GetInt("HightScore");
            hightTimeScore = PlayerPrefs.GetInt("HightTimeScore");
        }        
        if (player.GetComponent<Player>()._curGold <= 0)
        {
            if (score > hightScore) PlayerPrefs.SetInt("HightScore", score);
            if (timeScore > hightTimeScore) PlayerPrefs.SetInt("HightTimeScore", Mathf.CeilToInt(timeScore));
        }
        if (Input.GetKeyDown(KeyCode.F1)) PlayerPrefs.DeleteAll();
    }

}
