using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public bool isGrounded;

    float moveInput;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public GameObject jumpParticle;

    bool facingRight = true;
    bool jumped;

    int extraJumps;
    public int maxExtraJumps;

    Animator anim;
    public AudioSource audio;
    PlayerClassScript playerClassScript;
    void Start()
    {
        playerClassScript = GetComponent<PlayerClassScript>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //do zmiany jeżeli power up w sklepie
        maxExtraJumps = playerClassScript.playerClass.basicExtraJumps;
        extraJumps = maxExtraJumps;
        speed = playerClassScript.playerClass.speed;
        jumpForce = playerClassScript.playerClass.jumpForce;
    }

    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("GameManager").GetComponent<PausePanel>().paused)
        {
            if ((rb.velocity.y >= 0.1 && rb.velocity.y > 0) || (rb.velocity.y <= 0.1 && rb.velocity.y < 0) && !isGrounded)
            {
                anim.SetBool("isJumping", true);
            }
            else
            {
                anim.SetBool("isJumping", false);
            }

            if (isGrounded)
            {
                extraJumps = maxExtraJumps;
                anim.SetBool("isFalling", false);
            }

            if (Input.GetAxisRaw("Jump") > 0.1f && extraJumps > 0 && !jumped)
            {
                rb.velocity = Vector2.up * jumpForce;
                Instantiate(jumpParticle, groundCheck.transform.position, Quaternion.identity);
                extraJumps--;
                jumped = true;
            }
            else if (Input.GetAxisRaw("Jump") > 0.1f && extraJumps == 0 && isGrounded && !jumped)
            {
                rb.velocity = Vector2.up * jumpForce;
                Instantiate(jumpParticle, groundCheck.transform.position, Quaternion.identity);
                jumped = true;
            }

            if (jumped && Input.GetAxisRaw("Jump") < 0.1f)
            {
                jumped = false;
            }

            if (rb.velocity.y > 0.1f)
            {
                anim.SetBool("isFalling", false);
            }
            else if (rb.velocity.y < -0.1f)
            {
                anim.SetBool("isFalling", true);
            }
            if ((Input.GetAxisRaw("Shoot") > 0.5f && Input.GetAxisRaw("Shoot") > 0f) || (Input.GetAxisRaw("Shoot") < -0.5f && Input.GetAxisRaw("Shoot") < 0f))
            {
                if (!facingRight && Input.GetAxisRaw("Shoot") > 0.5f)
                {
                    Flip();
                }
                else if (facingRight && Input.GetAxisRaw("Shoot") < -0.5f)
                {
                    Flip();
                }
            }
            else
            {
                if (!facingRight && moveInput > 0)
                {
                    Flip();
                    anim.SetBool("isBack", false);
                }
                else if (facingRight && moveInput < 0)
                {
                    Flip();
                    anim.SetBool("isBack", false);
                }
            }

            if ((!facingRight && moveInput > 0) || (facingRight && moveInput < 0))
            {
                anim.SetBool("isBack", true);
            }

            if (moveInput == 0)
            {
                anim.SetBool("isBack", false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!GameObject.FindGameObjectWithTag("GameManager").GetComponent<PausePanel>().paused) 
        { 
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            moveInput = Input.GetAxisRaw("Horizontal");
            if (moveInput != 0)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
            rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}


