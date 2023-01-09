using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnType : MonoBehaviour
{
    public BTNtype currentType;

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNtype.NEW:
                Debug.Log("새로하기");
                break;
            case BTNtype.CONTINUE:
                Debug.Log("계속하기");
                break;
        }
    }
}
