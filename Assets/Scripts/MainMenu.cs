using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject contButton;
    public GameObject player;

    public void Start()
    {
        AudioManager.instance.PlayMainMenuMusic();
        if (PlayerPrefs.HasKey("ContinueScene"))
        {
            contButton.SetActive(true);
        }
    }

    public void playGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Main Level");
    }

    public void Settings()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        if (PlayerPrefs.HasKey("ContinueScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("ContinueScene"));
        }
        
            
    }
}
