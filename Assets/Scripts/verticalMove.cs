using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class verticalMove : MonoBehaviour
{
    const string UP = "Up";
    const string DOWN = "Down";

    [SerializeField]
    Transform castPos;

    [SerializeField]
    float baseCastDist;

    string direction;

    Vector3 baseScale;

    public Timeline time;

    public float moveSpeed = 1f;

    public LayerMask groundLayer;


    void Start()
    {
        baseScale = transform.localScale;
        direction = DOWN;

        time = GetComponent<Timeline>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float vY = moveSpeed;

        if (direction == DOWN)
        {
            vY = -moveSpeed;
        }

        time.rigidbody2D.velocity = new Vector2(0, vY);

        if (isHittingWall())
        {
            if (direction == DOWN)
            {
                changeDir(UP);
            }
            else
            {
                changeDir(DOWN);
            }
        }
    }

    void changeDir(string newDir)
    {
        Vector3 newScale = baseScale;

        if (newDir == DOWN)
        {
            newScale.y = baseScale.y;
        }
        else
        {
            newScale.y = -baseScale.y;
        }

        transform.localScale = newScale;
        direction = newDir;
    }



    bool isHittingWall()
    {
        bool val = false;

        float castDist = baseCastDist;

        if (direction == DOWN)
        {
            castDist = baseCastDist;
        }
        else
        {
            castDist = -baseCastDist;
        }

        Vector3 targetPos = castPos.position;
        targetPos.y += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("VGround")))
        {
            val = true;
        }
        else
        {
            val = false;
        }


        return val;
    }
}
