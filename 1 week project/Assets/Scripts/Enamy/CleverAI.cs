using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleverAI : MonoBehaviour
{
    
    //do naprawy:
    //jump point
    //wykrywanie obecnej platformy
    //skakanie


    Rigidbody2D rb;
    public GameObject[] platformsObjects;
    public List<GameObject> calculatedPlatforms;
    CameraEdgesColliders cameraEdges;
    Transform player;
    public GameObject currentEnamyPlatform;
    public GameObject currenPlayerPlatform;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnamy;

    public Transform walkCheck;
    public Transform groundCheck;
    public float checkRadius;
    bool isGrounded;
    public bool setup;
    bool sameY;

    public GameObject nearest;

    public GameObject debugSquare;

    public float speed;

    bool turnedRight = true;
    Vector2 jumpPoint;

    public Collider2D ground;
    public Collider2D playerGround;
    public GameObject nrst;
    public GameObject playerNrst;

    public List<GameObject> nearestCalculatedList;
    public GameObject nearestCalculated;
    public GameObject finalNearestPlatform;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraEdges = GameObject.FindGameObjectWithTag("EdgeController").GetComponent<CameraEdgesColliders>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        platformsObjects = GameObject.FindGameObjectsWithTag("Ground");

    }

    void FixedUpdate()
    {

        nrst = platformsObjects[0];
        foreach (GameObject platform in platformsObjects)
        {
            if (Vector2.Distance(platform.GetComponent<CompositeCollider2D>().bounds.center, transform.position) < (Vector2.Distance(nrst.GetComponent<CompositeCollider2D>().bounds.center, transform.position)))
            {
                nrst = platform;
            }
        }
        currentEnamyPlatform = nrst;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if(isGrounded && !setup)
        {
            setup = true;
        }

        foreach (GameObject platform in platformsObjects)
        {
            playerGround = Physics2D.OverlapCircle(player.GetComponent<PlayerMovement>().groundCheck.position, player.GetComponent<PlayerMovement>().checkRadius, whatIsGround);
            if (playerGround && playerGround.gameObject == platform && playerGround)
            {
                currenPlayerPlatform = playerGround.gameObject;
            }
        }
        if (setup) {
            if (!sameY)
            {
                if (transform.position.x < player.transform.position.x)
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                if (transform.position.x > player.transform.position.x)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }

                if (rb.velocity.x > 0 && !turnedRight)
                {
                    Flip();
                }
                else if(rb.velocity.x < 0 && turnedRight)
                {
                    Flip();
                }
                if (currenPlayerPlatform != currentEnamyPlatform)
                {
                    Debug.DrawLine(walkCheck.position, new Vector2(walkCheck.position.x, walkCheck.position.y - 1.3f));
                    if (!(Physics2D.Linecast(walkCheck.position, new Vector2(walkCheck.position.x, walkCheck.position.y - 1.3f), whatIsGround)))
                    {
                        /*
                        nearest = platformsObjects[0];
                        if(nearest = currentEnamyPlatform)
                        {
                            nearest = platformsObjects[1];
                        }
                        */
                        if (calculatedPlatforms.Count > 0) { calculatedPlatforms.Clear(); }
                        foreach (GameObject platformBefore in platformsObjects)
                        {
                            if (platformBefore != currentEnamyPlatform)
                            {
                                //z lewej
                                if(transform.position.x < player.transform.position.x)
                                {
                                    if (platformBefore.GetComponent<CompositeCollider2D>().bounds.center.x > currentEnamyPlatform.GetComponent<CompositeCollider2D>().bounds.center.x && currentEnamyPlatform != platformBefore)
                                    {
                                        if (transform.position.y > player.transform.position.y && platformBefore.GetComponent<CompositeCollider2D>().bounds.center.y < transform.position.y && platformBefore.GetComponent<CompositeCollider2D>().bounds.center.y > player.transform.position.y)
                                        {
                                            calculatedPlatforms.Add(platformBefore);
                                        }
                                        if (transform.position.y < player.transform.position.y && platformBefore.GetComponent<CompositeCollider2D>().bounds.center.y > transform.position.y && platformBefore.GetComponent<CompositeCollider2D>().bounds.center.y < player.transform.position.y)
                                        {
                                            calculatedPlatforms.Add(platformBefore);
                                        }
                                    }
                                }

                                //z prawej
                                if (transform.position.x > player.transform.position.x)
                                {
                                    if (platformBefore.GetComponent<CompositeCollider2D>().bounds.center.x < currentEnamyPlatform.GetComponent<CompositeCollider2D>().bounds.center.x && currentEnamyPlatform != platformBefore)
                                    {
                                    if (transform.position.y > player.transform.position.y && platformBefore.GetComponent<CompositeCollider2D>().bounds.center.y < transform.position.y && platformBefore.GetComponent<CompositeCollider2D>().bounds.center.y > player.transform.position.y)
                                        {
                                            calculatedPlatforms.Add(platformBefore);
                                        }
                                        if (transform.position.y < player.transform.position.y && platformBefore.GetComponent<CompositeCollider2D>().bounds.center.y > transform.position.y && platformBefore.GetComponent<CompositeCollider2D>().bounds.center.y < player.transform.position.y)
                                        {
                                            calculatedPlatforms.Add(platformBefore);
                                        }
                                    }
                                }
                            }
                        }
                        if (transform.position.x < player.transform.position.x)
                        {
                            if (nearestCalculatedList.Count > 0) nearestCalculatedList.Clear();
                            nearestCalculatedList.Add(calculatedPlatforms[0]);
                            nearestCalculated = calculatedPlatforms[0];
                            foreach (GameObject platform in calculatedPlatforms)
                            {
                                if (Mathf.Abs(platform.GetComponent<CompositeCollider2D>().bounds.center.y - currentEnamyPlatform.GetComponent<CompositeCollider2D>().bounds.center.y) <= Mathf.Abs(nearestCalculated.GetComponent<CompositeCollider2D>().bounds.center.y - currentEnamyPlatform.GetComponent<CompositeCollider2D>().bounds.center.y))
                                {
                                    nearestCalculatedList.Add(platform);
                                    nearestCalculated = platform;
                                }
                            }
                            finalNearestPlatform = nearestCalculatedList[0];
                            foreach (GameObject platform in nearestCalculatedList)
                            {
                                if (Mathf.Abs(platform.GetComponent<CompositeCollider2D>().bounds.center.x - currentEnamyPlatform.GetComponent<CompositeCollider2D>().bounds.center.x) < Mathf.Abs(finalNearestPlatform.GetComponent<CompositeCollider2D>().bounds.center.x - currentEnamyPlatform.GetComponent<CompositeCollider2D>().bounds.center.x))
                                {
                                    finalNearestPlatform = platform;
                                }
                            }
                        }
                        if (transform.position.x > player.transform.position.x)
                        {
                            if (nearestCalculatedList.Count > 0) nearestCalculatedList.Clear();
                            nearestCalculatedList.Add(calculatedPlatforms[0]);
                            nearestCalculated = calculatedPlatforms[0];
                            foreach (GameObject platform in calculatedPlatforms)
                            {
                                if (Mathf.Abs(platform.GetComponent<CompositeCollider2D>().bounds.center.y - currentEnamyPlatform.GetComponent<CompositeCollider2D>().bounds.center.y) <= Mathf.Abs(nearestCalculated.GetComponent<CompositeCollider2D>().bounds.center.y - currentEnamyPlatform.GetComponent<CompositeCollider2D>().bounds.center.y))
                                {
                                    nearestCalculatedList.Add(platform);
                                    nearestCalculated = platform;
                                }
                            }
                            finalNearestPlatform = nearestCalculatedList[0];
                            foreach (GameObject platform in nearestCalculatedList)
                            {
                                if (Mathf.Abs(platform.GetComponent<CompositeCollider2D>().bounds.center.x - currentEnamyPlatform.GetComponent<CompositeCollider2D>().bounds.center.x) < Mathf.Abs(finalNearestPlatform.GetComponent<CompositeCollider2D>().bounds.center.x - currentEnamyPlatform.GetComponent<CompositeCollider2D>().bounds.center.x))
                                {
                                    finalNearestPlatform = platform;
                                }
                            }
                        }

                        if (finalNearestPlatform != currentEnamyPlatform) {
                            if (transform.position.x < player.transform.position.x && isGrounded)
                            {
                                jumpPoint = new Vector2(finalNearestPlatform.GetComponent<CompositeCollider2D>().bounds.center.x - finalNearestPlatform.GetComponent<PlatformScript>().width / 2 + finalNearestPlatform.GetComponent<PlatformScript>().width * 0.1f, finalNearestPlatform.GetComponent<CompositeCollider2D>().bounds.center.y + finalNearestPlatform.GetComponent<PlatformScript>().height / 2);
                                Instantiate(debugSquare, jumpPoint, Quaternion.identity);
                                Debug.Log("JumpRight");
                                rb.velocity = 10f * Vector2.up;
                            }
                            if ((transform.position.x > player.transform.position.x && isGrounded))
                            { 
                                jumpPoint = new Vector2(finalNearestPlatform.GetComponent<CompositeCollider2D>().bounds.center.x + finalNearestPlatform.GetComponent<PlatformScript>().width / 2 - finalNearestPlatform.GetComponent<PlatformScript>().width * 0.1f, finalNearestPlatform.GetComponent<CompositeCollider2D>().bounds.center.y + finalNearestPlatform.GetComponent<PlatformScript>().height / 2);
                                Instantiate(debugSquare, jumpPoint, Quaternion.identity);
                                
                                Debug.Log("JumpLeft");
                                rb.velocity = 10f * Vector2.up;
                            }
                        }
                    }
                }
            }           
        }
    }

    void Flip()
    {
        turnedRight = !turnedRight;
        Vector3 scaler = transform.localScale;
        scaler.x = -scaler.x;
        transform.localScale = scaler;
    }

}
