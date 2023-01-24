using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcNameRot : MonoBehaviour
{
    [SerializeField] private Transform theCam;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(theCam);
    }
}
