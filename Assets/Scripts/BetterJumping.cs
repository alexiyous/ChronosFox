using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class BetterJumping : MonoBehaviour
{
   /* private Rigidbody2D time.rigidbody2D;*/
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public Timeline time;

    void Start()
    {
        /*time.rigidbody2D = GetComponent<Rigidbody2D>();*/
        time = GetComponent<Timeline>();
    }

    void Update()
    {
        if(time.rigidbody2D.velocity.y < 0)
        {
            time.rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if(time.rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            time.rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
