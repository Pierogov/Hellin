using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(-0.5f * Random.Range(400, 600)/100 * GameObject.FindGameObjectWithTag("Player").transform.localScale.x, 1f * Random.Range(400, 600) / 100);

        Physics2D.IgnoreLayerCollision(9, 13); 
        Physics2D.IgnoreLayerCollision(10, 13);

        Invoke("Die", 4);
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
