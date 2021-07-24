using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static bool isPause=false;
    public GameObject pausePanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {            
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPause = true;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Continue()
    {
        isPause = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    } 
    public void ExitGame()
    {       
        Application.Quit();
    }

    public void SoundVolume()
    {
        if (AudioListener.volume == 0) AudioListener.volume = 1;
        else if (AudioListener.volume == 1) AudioListener.volume = 0;
    }
}
