using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class projectile : MonoBehaviour
{

    public float startSpeed = 20f;
    public float maxSpeed = 40f;
    public float acceleration = 10f;
    private float currentSpeed;

    public GameObject impactEffect;
    public Timeline time;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        time = GetComponent<Timeline>();

        currentSpeed = startSpeed;

        Vector3 direction = player.position - transform.position;

        time.rigidbody2D.velocity = new Vector2(direction.x, direction.y).normalized * startSpeed;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);

        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentSpeed < maxSpeed)
        {
            currentSpeed = currentSpeed + acceleration * Time.deltaTime;
        }
        time.rigidbody2D.velocity = transform.up * currentSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFXAdjusted(6);
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            PlayerHealthController.instance.DamagePlayer(1);

        }

    }

}
