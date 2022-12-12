using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GameObject thePlayer;
    private void Update()
    {
        this.transform.position = thePlayer.transform.position;
    }
}
