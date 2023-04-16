using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCreditZone : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("end");
            Destroy(PlayerHealthController.instance.gameObject);//destroy the game object (player) so it doesnt spwan on the main menu
            PlayerHealthController.instance = null;//this is crucial in order to free up some spaces ini unity memory

            Destroy(RespawnController.instance.gameObject);//destroy the game object (Respawn poiny) so it doesnt spwan on the main menu
            RespawnController.instance = null;//this is crucial in order to free up some spaces ini unity memory
            SceneManager.LoadScene("Credits");
        }
    }
}
