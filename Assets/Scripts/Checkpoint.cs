using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RespawnController.instance.SetSpawn(transform.position);
            PlayerPrefs.SetFloat("PosX", transform.position.x);
            PlayerPrefs.SetFloat("PosY", transform.position.y);
            PlayerPrefs.SetFloat("PosZ", transform.position.z);
            PlayerPrefs.SetString("ContinueScene", "Main Level");
            PlayerPrefs.SetInt("Heart", PlayerHealthController.instance.playerHealth);
        }
    }
}
