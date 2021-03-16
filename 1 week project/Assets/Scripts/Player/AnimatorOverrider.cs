using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverrider : MonoBehaviour
{
    public Animator anim;

    public PlayerClassScript playerClassScript;
    void Start()
    {
        anim = GetComponent<Animator>();
        playerClassScript = GetComponent<PlayerClassScript>();
        Set();
    }

    public void Set()
    { 
        anim = GetComponent<Animator>();
        playerClassScript = GetComponent<PlayerClassScript>();
        anim.runtimeAnimatorController = playerClassScript.playerClass.animator;
        anim = GetComponent<Animator>();
        GetComponent<PlayerShooting>().anim = anim;
        GetComponent<PlayerShooting>().audio = GetComponent<AudioSource>();
    }
}
