using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineEndSignal : MonoBehaviour
{
    [SerializeField]
    private Camera cinematicCamera;
    [SerializeField]
    private CanvasGroup canvasGroup;
    public void EndSignal()
    {
        cinematicCamera.enabled = false;
        canvasGroup.alpha = 1;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
