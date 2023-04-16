using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void RespawnFill()
    {
        PlayerHealthController.instance.playerHealth = 3;
        FillHealth();
    }
}
