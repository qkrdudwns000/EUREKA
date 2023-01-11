using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineEndSignal : MonoBehaviour
{
    [SerializeField]
    private Camera cinematicCamera;
    public void EndSignal()
    {
        cinematicCamera.enabled = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
