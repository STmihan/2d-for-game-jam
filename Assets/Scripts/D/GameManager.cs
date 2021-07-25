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
    
    public bool isPause = false;

    [Header("Лучший результат:")]
    public int score=0;
    public int timeScore= 0;
    [SerializeField] Text txtScore, txtTimeScore;

    [Header("Heal bar и Game over:")]
    public Image Heal; //времянка
    public GameObject player;
    public GameObject game, over;
       
  

    void Awake()
    {
        if (_instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
  
    private void Update()
    {        
        Heal.fillAmount =  player.GetComponent<Player>()._curGold/ player.GetComponent<Player>().MaxGold;
        if(player.GetComponent<Player>()._curGold < 0)
        {
            game.SetActive(false);
                over.SetActive(true); 

            txtTimeScore.text = "Time: " + timeScore.ToString();
            txtScore.text = "Score: " + score.ToString();            

            Time.timeScale = 0f;
        }
    }
}
