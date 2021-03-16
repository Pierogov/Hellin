using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnamyAnimatorManagment : MonoBehaviour
{
    Transform player;
    public LayerMask whatIsSolid;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void EndEnteringPortal()
    {
        float randomizer = Random.Range(-300, 301) / 100;

        if (Physics2D.OverlapCircle(new Vector2(player.position.x + randomizer, transform.position.y), 0.2f, whatIsSolid))
        {
            transform.position = new Vector2(player.position.x + randomizer, transform.position.y + 3f);
        }
        else
        {
            transform.position = new Vector2(player.position.x + randomizer, transform.position.y);
        }
    }

    public void EndTeleporting()
    {
        gameObject.GetComponent<ShootingEnamy>().isTeleporting = false;
    }

    public void SetupSpawned()
    {
        GetComponent<ShootingEnamy>().setup = true;
        GetComponent<Animator>().SetTrigger("setup");
    }
}
