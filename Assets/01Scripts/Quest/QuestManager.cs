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

    //����Ʈ UI
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
        questList.Add(10, new QuestData("��� ���� �غ���.", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("ù ���� �ӹ�", new int[] { 1000, 1000 }));
        questList.Add(30, new QuestData("�ι�° ���� �ӹ�", new int[] { 1000, 1000 }));
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
                    text_Title.text = "����Ʈ�� ��ȭ�ϱ�";
                    text_Detail.text = "���� ���ú��� ���ʹ� ! \n�������� �̰����� �˷��شٰ� �Ѵ�. ���������� ���� ��ȭ�غ���";
                    text_RewardGold.text = "100���";
                }
                else if(questPopupIndex == 1)
                {
                    text_Title.text = "���󿡰� ��񱸸��ϱ�";
                    text_Detail.text = "�������� �����Ͽ� �ʿ��� ��� �����϶�� �����־���. ���󿡰԰��� ���⸦ �����غ���";
                    text_RewardGold.text = "����.";
                }
                break;
            case 20:
                if (questPopupIndex == 0)
                {
                    text_Title.text = "����Ʈ�� ��ȭ�ϱ�";
                    text_Detail.text = "��� �����ߴ�. �ٽ� ���������� ������.";
                    text_RewardGold.text = "����.";
                }
                else if (questPopupIndex == 1)
                {
                    text_Title.text = "����Ȳ�� ����ϱ�";
                    text_Detail.text = "���� ù �ӹ��� !\n�� ������ ��Ż�� ź �� ���������� ����Ȳ�� ��īŸ��罺�� ����ϰ����";
                    text_RewardGold.text = "1000���";
                }
                else if (questPopupIndex == 2)
                {
                    text_Title.text = "����Ʈ�� ��ȭ�ϱ�";
                    text_Detail.text = "����Ȳ�Ҹ� ����ߴ� !\n���������� ���� ������ �޵�������.";
                    text_RewardGold.text = "1000���";
                }
                break;
            case 30:
                if (questPopupIndex == 0)
                {
                    GameManager.Inst.Gold += 1000;
                    text_Title.text = "����Ʈ�� ��ȭ�ϱ�";
                    text_Detail.text = "������ ����ð��� ���� �������� ��ȭ�غ���";
                    text_RewardGold.text = "����";
                }
                else if (questPopupIndex == 1)
                {
                    text_Title.text = "������� ����ϱ�";
                    text_Detail.text = "�ι�° �ӹ��� !\n��������� ������� ������ ����ٰ��Ѵ�. ����� �������� ���� ������� �ôϰ��� ����� ����������.";
                    text_RewardGold.text = "2000���";
                }
                else if (questPopupIndex == 2)
                {
                    text_Title.text = "����Ʈ�� ��ȭ�ϱ�";
                    text_Detail.text = "�ôϰ��̸� ����ߴ� !\n���������� ���� �˸���.";
                    text_RewardGold.text = "2000���";
                }
                break;
            case 40:
                if(questPopupIndex == 0)
                {
                    GameManager.Inst.Gold += 2000;
                    text_Title.text = "ALL CLEAR";
                    text_Detail.text = "�� �̻� ������ ����Ʈ�� �����ϴ�.";
                    text_RewardGold.text = "����";
                }
                break;
        }
    }
    private void CurMapName()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
            text_CurrentMapName.text = "���ʸ���";
        else if (SceneManager.GetActiveScene().name == "BattleScene_1")
            text_CurrentMapName.text = "��������";
        else if (SceneManager.GetActiveScene().name == "BattleScene_2")
            text_CurrentMapName.text = "����� ����";
    }
}
