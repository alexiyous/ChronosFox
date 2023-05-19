using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public Image[] hearts;
    public GameObject pauseScreen;
    public Image fadeScreen;
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FillHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < PlayerHealthController.instance.playerHealth)
            {
                hearts[i].color = Color.white;
            }
            else if (i >= PlayerHealthController.instance.playerHealth)
            {
                hearts[i].color = Color.black;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//basiclly you choose which key on keyboard you wanted to set as an input to execute an
                                             //action, in this case its the escape key.
        {
            PauseUnpause();
        }
    }

    public void RespawnFill()
    {
        PlayerHealthController.instance.playerHealth = 3;
        FillHealth();
    }

    public void PauseUnpause()
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);//set active the pause screen if it haven't been activated

            Time.timeScale = 0f;//stop the frame flow of time
        }
        else
        {
            pauseScreen.SetActive(false);//unactive the pause screen if its already been activated

            Time.timeScale = 1f;//continue the frame flow of time
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; //continue the frame so it doesnt freezes the screen if creating another new game in main menu

        Destroy(PlayerHealthController.instance.gameObject);//destroy the game object (player) so it doesnt spwan on the main menu
        PlayerHealthController.instance = null;//this is crucial in order to free up some spaces ini unity memory

        Destroy(RespawnController.instance.gameObject);//destroy the game object (Respawn poiny) so it doesnt spwan on the main menu
        RespawnController.instance = null;//this is crucial in order to free up some spaces ini unity memory

        instance = null;//destroy the game object so it doesnt spwan on the main menu
        Destroy(gameObject);

        SceneManager.LoadScene("Main Menu");
    }

    private IEnumerator FadeIn()
    {
        Color startColor = Color.black;
        Color targetColor = Color.clear;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            fadeScreen.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeScreen.color = targetColor;
        fadeScreen.gameObject.SetActive(false);
    }
}
