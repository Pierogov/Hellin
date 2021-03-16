using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    public float diff;
    public Sprite img;
    public Sprite empty;
    public int number;

    int magazine;

    public float yOffset;
    public float xOffset;

    public GameObject imagePrefab;
    public GameObject container;

    public List<GameObject> bullets;

    PlayerClassScript playerClassScript;
    void Start()
    {
        playerClassScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClassScript>();

        img = playerClassScript.playerClass.uiShellSprite;
        if (playerClassScript.playerClass.isBulletSpriteSymetric)
        {
            xOffset = 0;
        }
        else
        {
            xOffset = -0.5f;
        }
        diff = playerClassScript.playerClass.bulletBarDiff;

        imagePrefab.GetComponent<Image>().sprite = img;

        container.GetComponent<RectTransform>().sizeDelta = new Vector2(imagePrefab.GetComponent<RectTransform>().rect.width + diff * (number - 1), imagePrefab.GetComponent<RectTransform>().rect.width);

        for(int i = 0; i < number; i++)
        {
            GameObject spawned = Instantiate(imagePrefab, container.transform.position, Quaternion.identity, container.transform);
            bullets.Add(spawned);
            spawned.GetComponent<RectTransform>().anchoredPosition = new Vector2(spawned.GetComponent<RectTransform>().rect.width/2 + diff * i, 0);
        }

        container.GetComponent<RectTransform>().anchoredPosition = container.GetComponent<RectTransform>().anchoredPosition + new Vector2(xOffset, yOffset);
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player")) magazine = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>().magazine;

        for (int i=0;i < bullets.Count; i++)
        {
            if(i < magazine)
            {
                bullets[i].GetComponent<Image>().sprite = img;
            }
            else
            {
                bullets[i].GetComponent<Image>().sprite = empty;
            }
        }
    }
}
