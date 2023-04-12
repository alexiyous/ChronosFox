using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class verticalMove : MonoBehaviour
{
    // Start is called before the first frame update

    public float startSpeed;

    private float moveTime;
    public float startMoveTime = 2f;

    public Timeline time;
    Rigidbody2D rb;

    void Start()
    {
        time = GetComponent<Timeline>();
        rb = GetComponent<Rigidbody2D>();
        //time.rigidbody2D.velocity = new Vector2(0, 1).normalized * startSpeed;
        rb.velocity = new Vector2(0, 1).normalized * startSpeed;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    void move()
    {
        if(moveTime <= 0)
        {
            //time.rigidbody2D.velocity *= -1;
            rb.velocity *= -1;
            moveTime = startMoveTime;
        } else
        {
            moveTime -= Time.deltaTime;
        }
    }
}
