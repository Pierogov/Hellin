using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public Transform gunPoint;
    public Transform shellPoint;

    public float maxTimeBtwShoots;
    float timeBtwShoots;

    public float fireRate;

    public GameObject shootPrefab;
    public GameObject shellPrefab;
    public GameObject flashPrefab;

    public int maxMagazineSize;
    public int magazine;
    public float reloadTime;
    float leftReloadTime;

    public Animator anim;
    public AudioSource audio;
    bool reloading;

    public Slider reloadBar;
    AmmoBar ammoBar;
    PlayerClassScript playerClassScript;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        playerClassScript = GetComponent<PlayerClassScript>();

        gunPoint.localPosition = playerClassScript.playerClass.gunPoint;
        shellPoint.localPosition = playerClassScript.playerClass.shellPoint;


        audio.clip = playerClassScript.playerClass.shootSound;
        maxTimeBtwShoots = playerClassScript.playerClass.maxTimeBtwShoots;
        maxMagazineSize = playerClassScript.playerClass.maxMagazineSize;
        reloadTime = playerClassScript.playerClass.reloadTime;

        anim = GetComponent<Animator>();

        audio = GetComponent<AudioSource>();
        GetComponent<AnimatorOverrider>().Set();
        magazine = maxMagazineSize;

        ammoBar = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AmmoBar>();
        ammoBar.number = maxMagazineSize;
        anim = GetComponent<Animator>();
        audio.clip = playerClassScript.playerClass.shootSound;
    }

    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("GameManager").GetComponent<PausePanel>().paused)
        {
            //reload
            if ((Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.R)) && !reloading)
            {
                magazine = 0;
                reloading = true;
                reloadBar.gameObject.SetActive(true);
                leftReloadTime = reloadTime;
                reloadBar.maxValue = reloadTime;
            }

            if (magazine > 0 && !reloading)
            {
                if ((Input.GetAxisRaw("Shoot") > 0.5f && Input.GetAxisRaw("Shoot") > 0f) || (Input.GetAxisRaw("Shoot") < -0.5f && Input.GetAxisRaw("Shoot") < 0f) )
                {
                    anim.SetBool("isShooting", true);
                    if (timeBtwShoots <= 0)
                    {
                        Instantiate(flashPrefab, gunPoint.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Player").transform);
                        Instantiate(shootPrefab, gunPoint.position, Quaternion.identity);
                        Instantiate(shellPrefab, shellPoint.position, Quaternion.identity);
                        audio.Play();
                        timeBtwShoots = maxTimeBtwShoots * fireRate;
                        magazine -= 1;
                        if (magazine <= 0)
                        {
                            reloading = true;
                            reloadBar.gameObject.SetActive(true);
                            leftReloadTime = reloadTime;
                            reloadBar.maxValue = reloadTime;
                        }
                    }
                }
                else
                {
                    anim.SetBool("isShooting", false);
                }
                timeBtwShoots -= Time.deltaTime;
            }
            else
            {
                if (leftReloadTime <= 0)
                {
                    leftReloadTime = reloadTime;
                    magazine = maxMagazineSize;
                    reloading = false;
                    reloadBar.gameObject.SetActive(false);
                }
                else
                {
                    leftReloadTime -= Time.deltaTime;
                    reloadBar.value = leftReloadTime;
                }
            }

        }
    }
}
