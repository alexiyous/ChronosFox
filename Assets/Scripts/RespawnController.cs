using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Chronos;

public class RespawnController : MonoBehaviour
{
    public static RespawnController instance;
    

    public void Awake()
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

    private GameObject thePlayer;
    private Vector3 respawnPoint;
    public float waitToRespawn;
    private Clock enemyTime;
    private Clock tembakanTime;
    private GameObject deathEffect;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.gameObject;
        respawnPoint = thePlayer.transform.position;
        enemyTime = Timekeeper.instance.Clock("Enemy");
        tembakanTime = Timekeeper.instance.Clock("Projectile");
        deathEffect = Resources.Load("Player Death Effect") as GameObject;
    }

    public void Respawn()
    {
        Instantiate(deathEffect, thePlayer.transform.position, Quaternion.identity);
        StartCoroutine(RespawnCo());
    }

    public void SetSpawn(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
    IEnumerator RespawnCo()
    {

        thePlayer.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//load the previously scene
        enemyTime.localTimeScale = 1f;
        tembakanTime.localTimeScale = 1f;
        thePlayer.transform.position = respawnPoint;//set the player position according to the respawn point
        thePlayer.SetActive(true);//activate the player
        UIController.instance.RespawnFill();
    }
}

