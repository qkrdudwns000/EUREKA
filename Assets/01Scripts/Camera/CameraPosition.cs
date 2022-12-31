using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private Transform playerPositon;

    private void Update()
    {
        transform.position = playerPositon.position + Vector3.up * 1.3f;
    }
}
