using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class MapButtonController : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public MapBtnType curMapType;
    public Transform buttonScale;
    private Vector3 orgScale;
    public GameObject go_MapPopup;
    public CanvasGroup mapGroup;
    public TMPro.TMP_Text mapName;

    


    private void Start()
    {
        orgScale = buttonScale.localScale;
    }
    public void OnBtnClick()
    {
        switch(curMapType)
        {
            case MapBtnType.Battle1:
                MapPopupOpen();
                mapName.text = "서쪽폐허로\n이동하시겠습니까?";
                break;
            case MapBtnType.Battle2:
                break;
        }
    }


    public void GotoBattleScene(int mapNum)
    {
        SceneLoaded.Inst._gold = GameManager.Inst.Gold;
        SceneLoaded.Inst._level = GameManager.Inst.levelSystem.GetLevelNumber();
        SceneLoaded.Inst._experience = GameManager.Inst.levelSystem.experience;
        SceneLoaded.Inst.SaveData();



        switch (mapNum)
        {
            case 1:
                MapZone.isWatchingMap = false;
                LoadingSceneController.LoadScend("BattleScene_1");
                break;
            case 2:
                LoadingSceneController.LoadScend("BattleScene_2");
                break;
        }
    }
    public void MapPopupOpen()
    {
        go_MapPopup.SetActive(true);
        mapGroup.blocksRaycasts = false;
    }
    public void MapPopupClose()
    {
        go_MapPopup.SetActive(false);
        mapGroup.blocksRaycasts = true;
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
