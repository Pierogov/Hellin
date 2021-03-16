using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class papa : MonoBehaviour
{

    public GameObject endendPanel;
    bool clicked;
    void Papa()
    {
        gameObject.SetActive(false);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {

        if (!GameObject.FindGameObjectWithTag("Player") && !clicked)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                clicked = true;
                endendPanel.SetActive(true);
            }
        }
    }
}
