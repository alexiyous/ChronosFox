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
  

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
    }

    public void Update()
    {
        if (playerHealth <= 0)
        {
            RespawnController.instance.Respawn();
        }
    }

    public void UpdateHealth()
    {
        UIController.instance.FillHealth();
    }

    public void DamagePlayer(int damage)
    {
            playerHealth -= damage;
            UpdateHealth(); 
    }
}
