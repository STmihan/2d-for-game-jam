using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    GameObject player;
    public AudioSource Mixer;
    
    [Header("Panels is GameUI:")]
    public GameObject pausePanel;
    public GameObject gamePanel;

    [Header("Heal bar:")]
    public Image Heal; 
    
    public GameObject gameUI, gameOverUI;

    public Text txtScore, txtTimeScore, txtHightScore, txtHightTimeScore;

    [Header("Pause")]
    public Button Home;
    public Button Mute;
    public Button Resume;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }    
    private void Update()
    {
        InputPause();
        ScoreHight();
        HealBar();
    }
    void HealBar()
    {
        Heal.fillAmount = player.GetComponent<Player>()._curGold / player.GetComponent<Player>().MaxGold;
    }
    void ScoreHight()
    {
        if (player.GetComponent<Player>()._curGold <= 0)
        {
            gameOverUI.SetActive(true);
            gameUI.SetActive(false);

            txtTimeScore.text = Mathf.CeilToInt(GameManager.Instance.timeScore).ToString();
            txtScore.text = GameManager.Instance.score.ToString();

            txtHightScore.text = GameManager.Instance.hightScore.ToString();
            txtHightTimeScore.text = GameManager.Instance.hightTimeScore.ToString();

            Time.timeScale = 0f;
            GameManager.Instance.isPause = true;
        }
    }
    void InputPause()
    {
        // && SceneManager.GetActiveScene().buildIndex != 0
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.isPause) Continue();
            else Stop();
        }
    }
    public void Continue()
    {
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 1f;
        GameManager.Instance.isPause = false;
    }
    public void Stop()
    {
        if (GameManager.Instance.isPause) Continue();
        else 
        {
            pausePanel.SetActive(true);
            gamePanel.SetActive(false);
            Time.timeScale = 0f;
            GameManager.Instance.isPause = true;
        }
       
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 1f;
        GameManager.Instance.isPause = false;
    }
    public void SoundVolume()
    {
        if (Mixer.mute) Mixer.mute = false;
        else Mixer.mute = true;
    }
}
