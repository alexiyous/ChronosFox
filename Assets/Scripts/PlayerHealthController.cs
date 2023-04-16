using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int playerHealth;

    public float invincibilityLength;
    private float invinceCounter;

    public float flashLength;
    private float flashCounter;

    public SpriteRenderer[] playerSprites;

    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.HasKey("Heart"))
        {
           playerHealth = PlayerPrefs.GetInt("Heart");
           UpdateHealth();
        }
       
        
        
    }

    public void Update()
    {
        if (invinceCounter > 0)
        {
            invinceCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                }
                flashCounter = flashLength;
            }

            if (invinceCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                }
                flashCounter = 0f;
            }
        }

        if (invinceCounter <= 0)
        {
            if (playerHealth <= 0)
            {

                /*gameObject.SetActive(false);*/

                RespawnController.instance.Respawn();

                /*AudioManager.instance.PlaySFX(8);//play player death SFX*/
            }
        }
    }

    public void UpdateHealth()
    {
        UIController.instance.FillHealth();
    }

    public void DamagePlayer(int damage)
    {
        
        if (invinceCounter <= 0)
        {
            playerHealth -= damage;
            UpdateHealth();
            AudioManager.instance.PlaySFXAdjusted(3);
            if (playerHealth > 0)
            {
                invinceCounter = invincibilityLength;

                /*   AudioManager.instance.PlaySFXAdjusted(11);//play player hurt SFX*/
            }
        }


    }
}
