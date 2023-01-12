using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapZone : MonoBehaviour
{
    static public bool isWatchingMap = false;
    [SerializeField]
    private GameObject go_Map;
    [SerializeField]
    private CanvasGroup theCanvas;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            go_Map.SetActive(true);
            theCanvas.alpha = 0;
            isWatchingMap = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            go_Map.SetActive(false);
            theCanvas.alpha = 1;
            isWatchingMap = false;
        }
        
    }
}
