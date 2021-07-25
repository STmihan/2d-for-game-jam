using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    
    public GameObject pausePanel,gamePanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
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
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Continue()
    {
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 1f;
        GameManager.Instance.isPause = false;
    } 
    public void ExitGame()
    {       
        Application.Quit();
    }

    public void SoundVolume()
    {
        if (AudioListener.volume == 0) AudioListener.volume = 1;
        else AudioListener.volume = 0;
    }
}
