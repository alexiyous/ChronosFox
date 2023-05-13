using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Chronos;

public class Movement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector]
   /* public Rigidbody2D rb;*/
    private AnimationScript anim;
    public Timeline time;
    public Clock enemyClock;
    public Clock projectileClock;
    public Clock musicTime;
    public bool isfreezed = false;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;
    public float freezeCounter = 0f;
    public float waitAfterFreeze = 0f;
    public float freezeEffect = 0f;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;
    public bool canFreeze = true;
    public bool isInSlowArea = false;

    [Space]

    private bool groundTouch;
    private bool hasDashed;

    public int side = 1;

    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;
    public GameObject walkParticle;



    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        /*rb = GetComponent<Rigidbody2D>();*/
        anim = GetComponentInChildren<AnimationScript>();
        time = GetComponent<Timeline>();
        enemyClock = Timekeeper.instance.Clock("Enemy");
        projectileClock = Timekeeper.instance.Clock("Projectile");
        musicTime = Timekeeper.instance.Clock("Music");
        if (PlayerPrefs.HasKey("PosX"))
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        GetTimekeeper();

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);
        anim.SetHorizontalMovement(x, y, time.rigidbody2D.velocity.y);

        if (canFreeze)
        {
            if (freezeCounter <= 0)
            {
                PlayerNotFreeze();
                freezeCounter = 0f;
                UnfreezeTime();
                if (Input.GetButton("Fire2") && Time.timeScale != 0f)
                {
                    shockwaveController.instance.CallShockwaveInverse();
                    AudioManager.instance.PlaySFXAdjusted(1);
                    FreezeTime();
                    freezeCounter = waitAfterFreeze;
                }
            } else if (freezeCounter > 0)
            {
                freezeCounter -= time.deltaTime;
            }
        }
        

        if (coll.onWall && Input.GetButton("Fire3") && canMove)
        {
            if(side != coll.wallSide)
                anim.Flip(side*-1);
            wallGrab = true;
            wallSlide = false;
        }

        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }
        
        if (wallGrab && !isDashing)
        {
            time.rigidbody2D.gravityScale = 0;
            if(x > .2f || x < -.2f)
            time.rigidbody2D.velocity = new Vector2(0, 0);

            float speedModifier = y > 0 ? .5f : 1;

            time.rigidbody2D.velocity = new Vector2(0, y * (speed * speedModifier));
        }
        else
        {
                time.rigidbody2D.gravityScale = 3;
            
        }

        if(coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (Input.GetButtonDown("Jump") && Time.timeScale != 0f)
        {
            AudioManager.instance.PlaySFXAdjusted(0);

            anim.SetTrigger("jump");

            if (coll.onGround)
                Jump(Vector2.up, false);
            if (coll.onWall && !coll.onGround)
                WallJump();
        }

        if (Input.GetButtonDown("Fire1") && !hasDashed && Time.timeScale != 0f)
        {
            AudioManager.instance.PlaySFXAdjusted(2);
            if(xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        WallParticle(y);

        if (wallGrab || wallSlide || !canMove)
            return;

        if(x > 0)
        {
            side = 1;
            anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            anim.Flip(side);
        }


    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;

        side = anim.sr.flipX ? -1 : 1;

        jumpParticle.Play();
    }

    public void GetTimekeeper()
    {
        if (enemyClock == null)
        {
            enemyClock = Timekeeper.instance.Clock("Enemy");
            projectileClock = Timekeeper.instance.Clock("Projectile");
            musicTime = Timekeeper.instance.Clock("Music");
        }
    }

    private void FreezeTime()
    {
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
        enemyClock.localTimeScale = 0.5f;
        projectileClock.localTimeScale = 0.33f;
        Debug.Log("Freeze");
        isfreezed = true;
        musicTime.localTimeScale = 0.5f;
        freezeEffect = 3f;

    }

    private void UnfreezeTime()
    {
        
        freezeEffect -= time.deltaTime;
        if (freezeEffect <= 0)
        {
            isfreezed = false;
            enemyClock.localTimeScale = 1f;
            projectileClock.localTimeScale = 1f;
            freezeEffect = 0f;
        }
       

    }

    private void Dash(float x, float y)
    {
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
        shockwaveController.instance.CallShockwave();

        hasDashed = true;

        anim.SetTrigger("dash");

        time.rigidbody2D.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        time.rigidbody2D.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        dashParticle.Play();
        time.rigidbody2D.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        dashParticle.Stop();
            time.rigidbody2D.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.5f);
        if (coll.onGround)
            hasDashed = false;
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    private void WallSlide()
    {
        if(coll.wallSide != side)
         anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if((time.rigidbody2D.velocity.x > 0 && coll.onRightWall) || (time.rigidbody2D.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : 0;

        time.rigidbody2D.velocity = new Vector2(0, -slideSpeed);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped)
        {
            time.rigidbody2D.velocity = new Vector2(dir.x * speed, time.rigidbody2D.velocity.y);
        }
        else
        {
            
            time.rigidbody2D.velocity = Vector2.Lerp(time.rigidbody2D.velocity, (new Vector2(dir.x * speed, time.rigidbody2D.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
        //StartCoroutine(WalkParticleTimer());
    }

    private void Jump(Vector2 dir, bool wall)
    {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        time.rigidbody2D.velocity = new Vector2(time.rigidbody2D.velocity.x, 0);
        time.rigidbody2D.velocity += dir * jumpForce;

        particle.Play();
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void RigidbodyDrag(float x)
    {
        time.rigidbody2D.drag = x;
    }

    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }

    private void PlayerNotFreeze()
    {
        if (!isInSlowArea && !isfreezed)
        {
            if (musicTime.localTimeScale != 1f)
            {
                musicTime.localTimeScale = 1f;
               /* Debug.Log("NotFreeze");*/
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slow"))
        {
            musicTime.localTimeScale = 0.5f;
            isInSlowArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slow"))
        {
                isInSlowArea = false;  
        }
    }

    private IEnumerator WalkParticleTimer()
    {
        yield return new WaitForSeconds(2f);
        if(coll.onGround)
        {
            Instantiate(walkParticle);
        }
        
    }
}
