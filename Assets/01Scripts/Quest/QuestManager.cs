using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public int questPopupIndex;
    public bool questComplete = true;
    public GameObject[] questObject;

    //퀘스트 UI
    public GameObject go_QuestPanel;
    public GameObject go_BaseUI;
    public static bool isQuestPopup = false;
    public TMPro.TMP_Text text_Title;
    public TMPro.TMP_Text text_Detail;
    public TMPro.TMP_Text text_RewardGold;
    public TMPro.TMP_Text text_CurrentMapName;

    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questComplete = true;
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }
    private void Start()
    {
        CurMapName();   
    }


    private void GenerateData()
    {
        questList.Add(10, new QuestData("장비 구매 해보기.", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("첫 헌터 임무", new int[] { 1000, 1000 }));
        questList.Add(30, new QuestData("두번째 헌터 임무", new int[] { 1000, 1000 }));
        questList.Add(40, new QuestData("All Clear", new int[] { 9999 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string  CheckQuest(int id)
    {
        //Next Talk Target
        if (id == questList[questId].npcId[questActionIndex] && questComplete)
        {
            questActionIndex++;
            questPopupIndex++;
        }

        ControlObject();

        //questTalk End
        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
            ControlQuestPopup();
        }

        return questList[questId].questName;
    }
    public string CheckQuest()
    {
        return questList[questId].questName;
    }
    private void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
        questPopupIndex = 0;
    }
    public void TryOpenQuestPopup()
    {
        if (!isQuestPopup)
            OpenQuestPopup();
        else
            CloseQuestPopup();
    }
    public void OpenQuestPopup()
    {
        SoundManager.inst.SFXPlay("MenuPopup");
        go_QuestPanel.SetActive(true);
        go_BaseUI.transform.SetSiblingIndex(6);
        ControlQuestPopup();
        isQuestPopup = true;
    }
    public void CloseQuestPopup()
    {
        go_QuestPanel.SetActive(false);
        isQuestPopup = false;
    }

    private void ControlObject()
    {
        switch(questId)
        {
            case 10:
                if (questActionIndex == 1)
                    GameManager.Inst.Gold += 100;
                break;
            case 20:
                if (questActionIndex == 1)
                {
                    questComplete = false;
                    questActionIndex--;
                }
                break;
            case 30:
                if (questActionIndex == 1)
                {
                    questComplete = false;
                    questActionIndex--;
                }
                break;
        }
    }
    private void ControlQuestPopup()
    {
        switch (questId)
        {
            case 10:
                if (questPopupIndex == 0)
                {
                    text_Title.text = "말쿠트와 대화하기";
                    text_Detail.text = "나도 오늘부터 헌터다 ! \n엘리제가 이것저것 알려준다고 한다. 엘리제에게 가서 대화해보자";
                    text_RewardGold.text = "100골드";
                }
                else if(questPopupIndex == 1)
                {
                    text_Title.text = "장비상에게 장비구매하기";
                    text_Detail.text = "엘리제가 헌터일에 필요한 장비를 구매하라고 돈을주었다. 장비상에게가서 무기를 구매해보자";
                    text_RewardGold.text = "없음.";
                }
                break;
            case 20:
                if (questPopupIndex == 0)
                {
                    text_Title.text = "말쿠트와 대화하기";
                    text_Detail.text = "장비를 구매했다. 다시 엘리제에게 가보자.";
                    text_RewardGold.text = "없음.";
                }
                else if (questPopupIndex == 1)
                {
                    text_Title.text = "성난황소 사냥하기";
                    text_Detail.text = "드디어 첫 임무다 !\n성 정문의 포탈을 탄 후 붉은폐허의 성난황소 아카타우루스를 사냥하고오자";
                    text_RewardGold.text = "1000골드";
                }
                else if (questPopupIndex == 2)
                {
                    text_Title.text = "말쿠트와 대화하기";
                    text_Detail.text = "성난황소를 사냥했다 !\n엘리제에게 가서 보상을 받도록하자.";
                    text_RewardGold.text = "1000골드";
                }
                break;
            case 30:
                if (questPopupIndex == 0)
                {
                    GameManager.Inst.Gold += 1000;
                    text_Title.text = "말쿠트와 대화하기";
                    text_Detail.text = "조금의 정비시간을 갖고 엘리제와 대화해보자";
                    text_RewardGold.text = "없음";
                }
                else if (questPopupIndex == 1)
                {
                    text_Title.text = "검은사신 사냥하기";
                    text_Detail.text = "두번째 임무다 !\n먼저출발한 토벌팀이 연락이 끊겼다고한다. 희생의 무덤으로 가서 검은사신 시니가미 토벌을 돕도록하자.";
                    text_RewardGold.text = "2000골드";
                }
                else if (questPopupIndex == 2)
                {
                    text_Title.text = "말쿠트와 대화하기";
                    text_Detail.text = "시니가미를 사냥했다 !\n엘리제에게 가서 알리자.";
                    text_RewardGold.text = "2000골드";
                }
                break;
            case 40:
                if(questPopupIndex == 0)
                {
                    GameManager.Inst.Gold += 2000;
                    text_Title.text = "ALL CLEAR";
                    text_Detail.text = "더 이상 수행할 퀘스트가 없습니다.";
                    text_RewardGold.text = "없음";
                }
                break;
        }
    }
    private void CurMapName()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
            text_CurrentMapName.text = "태초마을";
        else if (SceneManager.GetActiveScene().name == "BattleScene_1")
            text_CurrentMapName.text = "붉은폐허";
        else if (SceneManager.GetActiveScene().name == "BattleScene_2")
            text_CurrentMapName.text = "희생의 무덤";
    }
}
