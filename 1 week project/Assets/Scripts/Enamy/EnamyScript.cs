using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyScript : MonoBehaviour
{
    public int maxHealth;
    public float speed;
    public float jumpForce;

    Rigidbody2D rb;
    GameObject player;
    int health;
    bool isGrounded;
    bool sameLevel;
    bool backRay;

    bool setup;

    WaveEnamySpawn spawner;

    public LayerMask whatIsPlayer;
    public LayerMask whatIsGround;

    public GameObject groundCheck;
    public GameObject raycastCheck;
    public GameObject backRayCheck;
    public float groundCheckRadius;
    bool canJump;

    bool facingRight = true;

    public GameObject bloodParticle;

    public Material[] materials;
    bool dead;

    public List<GameObject> bulletsToIgnore;

    private void Start()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreLayerCollision(10, 12);


        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WaveEnamySpawn>();
        GetComponent<SpriteRenderer>().material = materials[0];
    }

    private void Update()
    {
        /*
        if (!dead && player) {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) > 0.3f && setup)
            {
                if (transform.position.x < player.transform.position.x)
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
            }

            if (Mathf.Abs(transform.position.x - player.transform.position.x) > 0.3f && setup)
            {
                if (facingRight && rb.velocity.x < 0)
                {
                    Flip();
                }
                else if (!facingRight && rb.velocity.x > 0)
                {
                    Flip();
                }
            }
            isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, whatIsGround);
            if(isGrounded && !setup)
            {
                setup = true;
            }
            canJump = Physics2D.Linecast(raycastCheck.transform.position, Vector2.up * 100f, whatIsGround);
            backRay = Physics2D.Linecast(backRayCheck.transform.position, Vector2.up * 100f, whatIsGround);


            if (Physics2D.Linecast(transform.position, Vector2.right * 100f, whatIsPlayer) || Physics2D.Linecast(transform.position, Vector2.left * 100f, whatIsPlayer))
            {
                sameLevel = true;
            }
            else
            {
                sameLevel = false;
            }

            if (isGrounded && canJump && !sameLevel && player.GetComponent<PlayerMovement>().isGrounded && !backRay && player && !dead)
                if(player.transform != null && transform != null) rb.velocity = Mathf.Sqrt(Mathf.Abs(player.transform.position.y - transform.position.y)) * 8f * Vector2.up;
            Debug.DrawRay(transform.position, Vector2.right * 100f);
            Debug.DrawRay(transform.position, Vector2.left * 100f);
            Debug.DrawRay(raycastCheck.transform.position, Vector2.up * 100f);
        }
        */
        
        //Debug.Log(isGrounded);
        //Debug.Log(canJump);
        //Debug.Log(sameLevel);
        //Debug.Log(player.GetComponent<PlayerMovement>().isGrounded);
        //Debug.Log(isGrounded && canJump && !Physics2D.Raycast(transform.position, Vector2.right * 100f, whatIsPlayer) && !Physics2D.Raycast(transform.position, Vector2.left * 100f, whatIsPlayer) && player.GetComponent<PlayerMovement>().isGrounded);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.GetComponent<ShootingEnamy>().setup)
        {
            if (collision.gameObject.GetComponent<PlayerScript>().godmode == false)
            {
                collision.gameObject.GetComponent<PlayerScript>().godmode = true;
                collision.gameObject.GetComponent<PlayerScript>().godModeTime = collision.gameObject.GetComponent<PlayerScript>().maxGodModeTime;
                collision.gameObject.GetComponent<PlayerScript>().godModeCircle.SetActive(true);
                player.GetComponent<PlayerScript>().DealDMG(1);
                StartCoroutine(Dead());
            }
        }
    }

    public void DealDMG(int amount)
    {
        health -= amount;
        StartCoroutine(Whiten());

        if (health <= 0)
        {
            StartCoroutine(Dead());
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    IEnumerator Whiten()
    {
        GetComponent<SpriteRenderer>().material = materials[1];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material = materials[0];
    }

    IEnumerator Dead()
    {
        dead = true;
        StartCoroutine(Whiten());
        yield return new WaitForSeconds(0.1f);
        spawner.enamies.Remove(gameObject);
        Instantiate(bloodParticle, new Vector2(transform.position.x + 0.2f, transform.position.y), Quaternion.identity);
        Destroy(gameObject);
    }
}
