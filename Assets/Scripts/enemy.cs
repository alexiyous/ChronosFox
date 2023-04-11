using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class enemy : MonoBehaviour
{
    // Start is called before the first frame update
    const string LEFT = "left";
    const string RIGHT = "right";

    [SerializeField]
    Transform castPos;

    [SerializeField]
    float baseCastDist;

    string direction;

    Vector3 baseScale;

    public float moveSpeed = 5f;
    public float downDist;
    public Timeline time;

    public LayerMask groundLayer;
    void Start()
    {
        baseScale = transform.localScale;
        direction = RIGHT;

        time = GetComponent<Timeline>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float vX = moveSpeed;

        if(direction == LEFT)
        {
            vX = -moveSpeed;
        }

        time.rigidbody2D.velocity = new Vector2(vX, time.rigidbody2D.velocity.y);
        time.rigidbody2D.gravityScale = 1f;

        if((isHittingWall() || isNearEdge()) && isGrounded())
        {
            if(direction == LEFT)
            {
                changeDir(RIGHT);
            } else
            {
                changeDir(LEFT);
            }
        }
    }

    void changeDir(string newDir)
    {
        Vector3 newScale = baseScale;

        if(newDir == LEFT)
        {
            newScale.x = -baseScale.x;
        } else
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;
        direction = newDir;
    }

    bool isHittingWall()
    {
        bool val = false;

        float castDist = baseCastDist;

        if(direction == LEFT)
        {
            castDist = -baseCastDist;
        } else
        {
            castDist = baseCastDist;
        }

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);

        if(Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        } else
        {
            val = false;
        }


        return val;
    }

    bool isNearEdge()
    {
        bool val = true;

        float castDist = baseCastDist;

        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }


        return val;
    }

    bool isGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = downDist;

        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if(hit.collider != null)
        {
            return true;
        }
        return false;

        
    }
}
