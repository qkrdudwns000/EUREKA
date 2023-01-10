using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapZone : MonoBehaviour
{
    static public bool isWatchingMap = false;
    [SerializeField]
    private GameObject go_Map;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            go_Map.SetActive(true);
            isWatchingMap = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            go_Map.SetActive(false);
            isWatchingMap = false;
        }
        
    }
}
