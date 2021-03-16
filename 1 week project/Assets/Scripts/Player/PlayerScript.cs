using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public bool godmode;

    public float maxGodModeTime;
    public float godModeTime;

    public Material[] materials;

    public GameObject bloodParticle;

    public bool dead;

    PlayerClassScript playerClassScript;
    HealthBar healthBar;

    public GameObject godModeCircle;

    public GameObject endPanel;

    void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HealthBar>();
        playerClassScript = GetComponent<PlayerClassScript>();
        maxHealth = playerClassScript.playerClass.maxHealth;
        health = maxHealth;
        healthBar.number = maxHealth;

    }

    void Update()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if (!GameObject.FindGameObjectWithTag("GameManager").GetComponent<PausePanel>().paused)
        {
            if (godmode)
            {
                Physics2D.IgnoreLayerCollision(9, 10);
                if (godModeTime <= 0)
                {
                    godmode = false;
                }
                else
                {
                    if (godModeTime < 0.5 * maxGodModeTime)
                    {
                        godModeCircle.active = !godModeCircle.active;
                    }
                    godModeTime -= Time.deltaTime;
                }
            }
            else
            {
                Physics2D.IgnoreLayerCollision(9, 10, false);
                godModeCircle.SetActive(false);
            }
            if (health <= 0 && !dead)
            {
                //restart
                StartCoroutine(Dead());
            }
        }
    }

    public void DealDMG(int amount)
    {
        health -= amount;
        StartCoroutine(Whiten());
    }

    IEnumerator Whiten()
    {
        GetComponent<SpriteRenderer>().material = materials[1];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material = materials[0];
    }

    IEnumerator Dead()
    {
        FindObjectOfType<WaveEnamySpawn>().finalWave = FindObjectOfType<WaveEnamySpawn>().wave;
        if(FindObjectOfType<WaveEnamySpawn>().finalWave > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", FindObjectOfType<WaveEnamySpawn>().finalWave);
        }
        GameObject.FindGameObjectWithTag("MusicMan").GetComponent<Animator>().SetTrigger("end");
        dead = true;
        StartCoroutine(Whiten());
        Instantiate(bloodParticle, new Vector2(transform.position.x + 0.2f, transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        endPanel.SetActive(true);
        Destroy(gameObject);
    }
}
