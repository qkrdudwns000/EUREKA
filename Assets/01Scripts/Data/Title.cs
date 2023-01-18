using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public static Title inst;

    private SaveNLoad theSaveNLoad;

    private void Awake()
    {
        theSaveNLoad = FindObjectOfType<SaveNLoad>();

        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    
}
