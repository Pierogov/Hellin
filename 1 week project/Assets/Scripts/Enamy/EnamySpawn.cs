using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnamySpawn : MonoBehaviour
{
    public int limit;
    public float startDelay;
    float delay;
    float timeToSpawn;
    public float maxTimeToSpawn;

    float startTimeToSpawn;
    public float maxStartTimeToSpawn;

    public float startAmount;

    float randomizer;
    public float randOffset;

    CameraEdgesColliders camEdges;

    public int totalAmount;
    int amount;

    public List<GameObject> enamies;

    public GameObject[] zombiesVariants;

    private void Start()
    {
        delay = startDelay;
        timeToSpawn = maxTimeToSpawn;
        startTimeToSpawn = maxStartTimeToSpawn;

        amount = totalAmount;

        camEdges = GameObject.FindGameObjectWithTag("EdgeController").GetComponent<CameraEdgesColliders>();

    }

    void Update()
    {
        if (delay <= 0)
        {
            if (amount > 0)
            {
                if (startAmount > 0)
                {
                    if (startTimeToSpawn <= 0)
                    {
                        if (enamies.Count < limit)
                        {
                            randomizer = (Random.Range(randOffset * 100, 100 - randOffset * 100)) / 100;
                            GameObject spawned = Instantiate(zombiesVariants[Random.Range(0, zombiesVariants.Length)], new Vector2(camEdges.topLeft.x + (camEdges.topRight.x - camEdges.topLeft.x) * randomizer, camEdges.topLeft.y + 1f), Quaternion.identity);
                            enamies.Add(spawned);
                            startAmount -= 1;
                            amount -= 1;
                        }
                        startTimeToSpawn = maxStartTimeToSpawn;
                    }
                    else
                    {
                        startTimeToSpawn -= Time.deltaTime;
                    }
                }
                else
                {
                    if (timeToSpawn <= 0)
                    {
                        if (enamies.Count < limit)
                        {
                            randomizer = (Random.Range(randOffset * 100, 100 - randOffset * 100)) / 100;
                            GameObject spawned = Instantiate(zombiesVariants[Random.Range(0, zombiesVariants.Length)], new Vector2(camEdges.topLeft.x + (camEdges.topRight.x - camEdges.topLeft.x) * randomizer, camEdges.topLeft.y + 1f), Quaternion.identity);
                            enamies.Add(spawned);
                            amount -= 1;
                        }
                        timeToSpawn = maxTimeToSpawn;
                    }
                    else
                    {
                        timeToSpawn -= Time.deltaTime;
                    }
                }
            }

            if (amount <= 0 && enamies.Count == 0)
            {
                //Win
                Debug.Log("Win!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            delay -= Time.deltaTime;
        }
    }   
}
