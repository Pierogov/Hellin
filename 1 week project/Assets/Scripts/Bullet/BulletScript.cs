using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;
    public float lifeTime;

    GameObject shootParticle;

    GameObject player;
    PlayerClassScript playerClassScript;
    void Start()
    {
        shootParticle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MapScript>().shootParticle;

        player = GameObject.FindGameObjectWithTag("Player");
        playerClassScript = player.GetComponent<PlayerClassScript>();

        speed = playerClassScript.playerClass.bulletSpeed;
        lifeTime = playerClassScript.playerClass.bulletLifeTime;

        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(speed * player.transform.localScale.x, rb.velocity.y);

        transform.localScale = new Vector3(player.transform.localScale.x, 1f, 1f);

        Invoke("Die", lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enamy"))
        {
            if (!collision.gameObject.GetComponent<EnamyScript>().bulletsToIgnore.Contains(gameObject)) 
            {
                collision.GetComponent<EnamyScript>().DealDMG(playerClassScript.playerClass.damage);
                if (!playerClassScript.playerClass.penetrable)
                {
                    Destroy(gameObject);
                }
                collision.gameObject.GetComponent<EnamyScript>().bulletsToIgnore.Add(gameObject);
            }
        }

        if (collision.CompareTag("Ground"))
        {
            Instantiate(shootParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
