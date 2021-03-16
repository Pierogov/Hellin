using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Class", menuName ="Class")]
public class PlayerClass : ScriptableObject
{
    //Transforms
    public Vector3 gunPoint;
    public Vector3 shellPoint;

    //Animations
    public AnimatorOverrideController animator;

    //movement
    public float speed;
    public float jumpForce;
    public int basicExtraJumps;

    //shooting
    public bool penetrable;
    public int damage;
    public float bulletSpeed;
    public float bulletLifeTime;
    public float maxTimeBtwShoots;
    public int maxMagazineSize;
    public float reloadTime;
    public AudioClip shootSound;

    //healthbar
    public Sprite uiHealthSprite;
    public Sprite emptyUiHealthSprite;
    public bool isHearthSpriteSymetric;
    public float hearthBarDiff;


    //ammobar
    public Sprite uiShellSprite;
    public bool isBulletSpriteSymetric;
    public float bulletBarDiff;

    //hp
    public int maxHealth; 

}
