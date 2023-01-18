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
    public GameObject go_MapPopup;
    public CanvasGroup mapGroup;
    private QuestManager theQuestManager;
    public int mapNum;

    private void Awake()
    {
        theQuestManager = FindObjectOfType<QuestManager>();
    }
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
            MapPopupClose();
            go_Map.SetActive(false);
            theCanvas.alpha = 1;
            isWatchingMap = false;
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

    public void GotoBattleScene()
    {
        SceneLoaded.Inst._gold = GameManager.Inst.Gold;
        SceneLoaded.Inst._level = GameManager.Inst.levelSystem.GetLevelNumber();
        SceneLoaded.Inst._experience = GameManager.Inst.levelSystem.experience;
        SceneLoaded.Inst._skillPoint = GameManager.Inst.SkillPoint;
        SceneLoaded.Inst._questId = theQuestManager.questId;
        SceneLoaded.Inst._questActionIndex = theQuestManager.questActionIndex;
        SceneLoaded.Inst._questPopupIndex = theQuestManager.questPopupIndex;
        SceneLoaded.Inst._questComplete = theQuestManager.questComplete;
        SceneLoaded.Inst.SaveData();



        switch (mapNum)
        {
            case 1:
                MapZone.isWatchingMap = false;
                LoadingSceneController.LoadScend("BattleScene_1",false);
                break;
            case 2:
                MapZone.isWatchingMap = false;
                LoadingSceneController.LoadScend("BattleScene_2", false);
                break;
        }
    }
}
