using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndPanelTexts : MonoBehaviour
{
    public Text highscore;
    public Text score;

    float finalWave;

    private void Start()
    {
        finalWave = FindObjectOfType<WaveEnamySpawn>().finalWave;
    }
    void Update()
    {
        highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
        score.text = "Score: " + finalWave;
    }
}
