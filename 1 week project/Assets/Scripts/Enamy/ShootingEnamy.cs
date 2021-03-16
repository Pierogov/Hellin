using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnamy : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    GameObject player;

    public GameObject bulletPrefab;
    public Transform gunPoint;

    public float starTimeBtwShoots;
    float timeBtwShoots;

    public LayerMask whatIsGround;
    public Transform groundCheck;
    bool isGrounded;
    public float checkRadius;

    public bool setup;
    bool turnedRight = true;

    public bool isTeleporting;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        timeBtwShoots = starTimeBtwShoots;
    }

    void Update()
    {

        if (setup && !isTeleporting && player)
        {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) > 0.3)
            {
                if (transform.position.x > player.transform.position.x)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
            }
            if (rb.velocity.x > 0 && !turnedRight)
            {
                Flip();
            }
            else if (rb.velocity.x < 0 && turnedRight)
            {
                Flip();
            }
            /*
            if (timeBtwShoots <= 0)
            {
                if (!Physics2D.Linecast(transform.position, player.transform.position, whatIsGround))
                {
                    Vector3 diff = player.transform.position - transform.position;
                    float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90f;

                    Instantiate(bulletPrefab, gunPoint.position, Quaternion.Euler(0f, 0f, rotZ));
                    timeBtwShoots = starTimeBtwShoots;
                }
            }
            else
            {
                timeBtwShoots -= Time.deltaTime;
            }
            */
        }
    }

    void Flip()
    {
        turnedRight = !turnedRight;
        Vector3 scaler = transform.localScale;
        scaler.x = -scaler.x;
        transform.localScale = scaler;
    }

    void Teleport()
    {
        
    }
}
