using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public static DataBase inst;
    
    private void Awake()
    {
        if (inst = null)
            inst = this;
        else
            Destroy(this.gameObject);
    }
    public static DataBase Inst
    {
        get
        {
            if (inst == null)
                return null;

            return inst;
        }
    }

    public void RelayData()
    {
        
    }
}
