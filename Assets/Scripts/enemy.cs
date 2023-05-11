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

    public Timeline time;

    public float moveSpeed = 5f;
    public float downDist;
    public int damageDealt;

    public LayerMask groundLayer;

    private Animator anim;

    private SpriteRenderer sr;

    private bool isDead;

    void Start()
    {
        baseScale = transform.localScale;
        direction = RIGHT;

        time = GetComponent<Timeline>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isDead)
        {
            return;
        }

        float vX = moveSpeed;

        if(direction == LEFT)
        {
            vX = -moveSpeed;
        }

        time.rigidbody2D.velocity = new Vector2(vX, time.rigidbody2D.velocity.y);

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

        int mask1 = 1 << LayerMask.NameToLayer("Ground");
        int mask2 = 1 << LayerMask.NameToLayer("Player");
        int mask3 = 1 << LayerMask.NameToLayer("Ground NC");
        int maskCombined = mask1 | mask2 | mask3;

        if (Physics2D.Linecast(castPos.position, targetPos, maskCombined))
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

        if(isHittingWall())
        {
            return true;
        }

        if(hit.collider != null)
        {
            return true;
        }
        return false;
    }

    public void Die()
    {
        if(!isDead)
        {
            AudioManager.instance.PlaySFXAdjusted(4);
        }
        
        sr.flipY = true;
        anim.enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        moveSpeed = 0f;
        Destroy(gameObject, 3f);
        isDead = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer(damageDealt);
        }
    }
}
