using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class MapButtonController : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public MapBtnType curMapType;
    public Transform buttonScale;
    private Vector3 orgScale;
    public MapZone theMap;
    public TMPro.TMP_Text mapName;
    public int mapNum;
    


    private void Start()
    {
        orgScale = buttonScale.localScale;
    }
    public void OnBtnClick()
    {
        switch(curMapType)
        {
            case MapBtnType.Battle1:
                theMap.MapPopupOpen();
                theMap.mapNum = 1;
                mapName.text = "���������\n�̵��Ͻðڽ��ϱ�?";
                break;
            case MapBtnType.Battle2:
                theMap.MapPopupOpen();
                theMap.mapNum = 2;
                mapName.text = "����� ��������\n�̵��Ͻðڽ��ϱ�?";
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = orgScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = orgScale;
    }
}
