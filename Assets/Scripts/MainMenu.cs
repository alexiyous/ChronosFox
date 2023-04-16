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
        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
        if (PlayerPrefs.HasKey("Heart"))
        {
            PlayerHealthController.instance.playerHealth = PlayerPrefs.GetInt("Heart");
            PlayerHealthController.instance.UpdateHealth();
        }
            
    }
}
