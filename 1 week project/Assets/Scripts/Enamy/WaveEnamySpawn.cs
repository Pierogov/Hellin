using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveEnamySpawn : MonoBehaviour
{
    public int limit;
    
    public float startDelay;
    float delay;

    public float maxStartTimeToSpawn;
    float startTimeToSpawn;

    public float maxTimeToSpawn;
    float timeToSpawn;

    public int amount;
    public int totalAmount;

    int maxWaves;
    public int wave;

    public int randomizer;
    public int randomizerZombie;

    public Transform[] spawnPoints;
    public List<GameObject> enamies;
    public GameObject[] zombiesVariants;

    public GameObject wavePanel;
    public Text waveTextOnPanel;
    public Text dayText;
    public int finalWave;
    void Start()
    {
        delay = startDelay;
        timeToSpawn = maxTimeToSpawn;
        startTimeToSpawn = maxStartTimeToSpawn;

        NextWave();
        maxWaves = 3;

        CalcAm();
    }

    void Update()
    {
        if (wave <= maxWaves) {
            if (delay <= 0)
            {
                if (totalAmount > 0)
                {
                    if (amount > 0)
                    {
                        if (startTimeToSpawn <= 0)
                        {
                            if (enamies.Count <= limit)
                            {
                                Debug.Log("StartSpawn");
                                Spawn();
                                amount -= 1;
                                totalAmount -= 1;
                                startTimeToSpawn = maxStartTimeToSpawn;
                            }
                        }
                        else
                        {
                            startTimeToSpawn -= Time.deltaTime;
                        }
                    }
                    if(amount == 0 && totalAmount > 0)
                    {
                        if (timeToSpawn <= 0)
                        {
                            if (enamies.Count <= limit)
                            {
                                Debug.Log("Spawn");
                                Spawn();
                                totalAmount -= 1;
                                timeToSpawn = maxTimeToSpawn;
                            }
                        }
                        else
                        {
                            timeToSpawn -= Time.deltaTime;
                        }
                    }
                }
                else
                {
                    if (enamies.Count <= 0)
                    {
                        //koniec fali
                        NextWave();
                    }
                }
            }
            else
            {
                delay -= Time.deltaTime;
            } 
        }
        else
        {
            Debug.Log("Koniec");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void Spawn()
    {
        randomizer = Random.Range(0, spawnPoints.Length);
        randomizerZombie = Random.Range(0, zombiesVariants.Length);

        GameObject spawnedZombie = Instantiate(zombiesVariants[randomizerZombie], spawnPoints[randomizer].position, Quaternion.identity);
        enamies.Add(spawnedZombie);

        if(FindObjectOfType<PlayerMovement>()) GetComponent<AudioSource>().Play();
    }

    void CalcAm()
    {
        //totalAmount = 3 + 3 * (wave - 1) + PlayerPrefs.GetInt("Day") + (int)Mathf.Ceil(((PlayerPrefs.GetInt("Day") - 1) / 3) * 1.5f);
        //amount = (int)Mathf.Ceil(0.4f * totalAmount);
        totalAmount = 5 + 3 * wave;
    }

    void NextWave()
    {
        wave += 1;
        maxWaves = wave + 1;
        waveTextOnPanel.text = "WAVE " + wave;
        wavePanel.SetActive(true);
        CalcAm();
        if ((wave - 1) % 3 == 0 && wave != 1) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().health += 1;
        }
    }
}
