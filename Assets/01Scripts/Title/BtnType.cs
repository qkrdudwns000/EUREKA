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
                Debug.Log("�����ϱ�");
                break;
            case BTNtype.CONTINUE:
                Debug.Log("����ϱ�");
                break;
        }
    }
}
