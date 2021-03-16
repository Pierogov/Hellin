using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float diff;
    public Sprite img;
    public Sprite empty;
    public int number;

    int health;

    public float yOffset;
    public float xOffset;

    public GameObject imagePrefab;
    public GameObject container;

    public List<GameObject> hearts;

    PlayerClassScript playerClassScript;
    void Start()
    {
        playerClassScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClassScript>();
        number = playerClassScript.playerClass.maxHealth;
        img = playerClassScript.playerClass.uiHealthSprite;
        empty = playerClassScript.playerClass.emptyUiHealthSprite;
        if (playerClassScript.playerClass.isHearthSpriteSymetric)
        {
            xOffset = 0;
        }
        else
        {
            xOffset = -0.5f;
        }
        diff = playerClassScript.playerClass.hearthBarDiff;

        imagePrefab.GetComponent<Image>().sprite = img;

        container.GetComponent<RectTransform>().sizeDelta = new Vector2(imagePrefab.GetComponent<RectTransform>().rect.width + diff * (number - 1), imagePrefab.GetComponent<RectTransform>().rect.width);

        for (int i = 0; i < number; i++)
        {
            GameObject spawned = Instantiate(imagePrefab, container.transform.position, Quaternion.identity, container.transform);
            hearts.Add(spawned);
            spawned.GetComponent<RectTransform>().anchoredPosition = new Vector2(spawned.GetComponent<RectTransform>().rect.width / 2 + diff * i, 0);
        }

        container.GetComponent<RectTransform>().anchoredPosition = container.GetComponent<RectTransform>().anchoredPosition + new Vector2(xOffset, yOffset);
    }

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player")) health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().health;

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < health)
            {
                hearts[i].GetComponent<Image>().sprite = img;
            }
            else
            {
                hearts[i].GetComponent<Image>().sprite = empty;
            }
        }
    }
}
