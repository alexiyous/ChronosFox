using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject contButton;
    public GameObject player;
    public Image fadeScreen;
    public float fadeDuration = 1f;

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
        StartCoroutine(Transition1());
        
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
        StartCoroutine(Transition2());


    }

    private IEnumerator Transition1()
    {
        yield return StartCoroutine(FadeOut());

        // Load the new scene here
       /* SceneManager.LoadScene("Main Level");*/
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Level");
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                // Scene loading is almost complete, activate it
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }


        /*yield return StartCoroutine(FadeIn());*/

        // Transition completed, do any necessary post-loading actions
    }

    private IEnumerator Transition2()
    {
        yield return StartCoroutine(FadeOut());

        // Load the new scene here
        /* SceneManager.LoadScene("Main Level");*/
        if (PlayerPrefs.HasKey("ContinueScene"))
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Level");
            asyncLoad.allowSceneActivation = false;

            while (!asyncLoad.isDone)
            {
                if (asyncLoad.progress >= 0.9f)
                {
                    // Scene loading is almost complete, activate it
                    asyncLoad.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }

    /* private IEnumerator FadeIn()
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
     }*/

    private IEnumerator FadeOut()
    {
        fadeScreen.gameObject.SetActive(true);
        Color startColor = Color.clear;
        Color targetColor = Color.black;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            fadeScreen.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeScreen.color = targetColor;
    }
}
