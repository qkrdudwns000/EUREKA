using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public bool questComplete = true;
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questComplete = true;
        questList = new Dictionary<int, QuestData>();
        GenerateData();
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
        if(id == questList[questId].npcId[questActionIndex] && questComplete)
            questActionIndex++;

        ControlObject();

        //questTalk End
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

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
    }

    private void ControlObject()
    {
        switch(questId)
        {
            case 10:
                if (questActionIndex == 1)
                    GameManager.Inst.Gold += 10000;
                break;
            case 20:
                if (questActionIndex == 1)
                {
                    questComplete = false;
                    questActionIndex--;
                }
                break;
            case 30:
                if(questActionIndex == 1)
                {
                    questComplete = false;
                    questActionIndex--;
                }
                break;
        }
    }
}
