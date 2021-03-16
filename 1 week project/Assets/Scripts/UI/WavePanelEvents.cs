using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePanelEvents : MonoBehaviour
{
    public void End()
    {
        gameObject.SetActive(false);
    }
}
