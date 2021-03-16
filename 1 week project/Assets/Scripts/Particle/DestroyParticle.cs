using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    ParticleSystem particle;
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        Invoke("Die", particle.startLifetime);
    }
    
    void Die()
    {
        Destroy(gameObject);
    }
}
