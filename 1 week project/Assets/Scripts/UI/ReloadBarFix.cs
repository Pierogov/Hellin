using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBarFix : MonoBehaviour
{
    Vector3 scaler;
    private void Awake()
    {
        scaler = transform.localScale;
    }

    private void LateUpdate()
    {
        if(GameObject.FindGameObjectWithTag("Player").transform.localScale.x > 0)
        {
            transform.localScale = scaler;
        }
        else
        {
            Vector3 adj = scaler;
            adj.x = -scaler.x;
            transform.localScale = adj;
        }
    }
}
