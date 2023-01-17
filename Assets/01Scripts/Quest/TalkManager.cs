using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        //Talk Data
        talkData.Add(1000, new string[] { "�ȳ�:0", "���� ������:0" });
        talkData.Add(2000, new string[] { "�� ~ ! �������� ������ ���� ����� ~ !:0" });

        //QuestTalk Data
        talkData.Add(10 + 1000, new string[] { "��! ����ī����\n���Ϳ� �����ߴٴ� �ҹ��� �����.:0",
                                               "������ ���⵵ ���� ����� �����ٴ� ���� ���� �ȵ���\n���� ���ο��Լ� ����� ���и� �纸������:0"});
        talkData.Add(11 + 2000, new string[] { "���� ~ ���� ~ ���� ���°� �����ϴٿ�~:0",
                                               "�� ���� ������ ���ͽŰ� ������ ?? \n�� ���� �帱�״� ���� ���ʽÿ� ~:0"});
        talkData.Add(20 + 1000, new string[] { "�� ~ ���⸦ ������ ���� ���� �ٿ� ������\nù ����� �����غ��� �׷��� �����Ա��� ���� ���� �������� �̵��� �� �ִ�.:0",
                                               "���������� �� �༮�� ����ϰ� ���ֱ� �ٷ�\n�ε� �¸��ϰ� ���ƿ��� . .:0"});
        talkData.Add(21 + 1000, new string[]{ "��! ����ī ù �����ӹ��� ������ �س±��� \n�ʵ� ���� ��� ���;� ����ī:0",
                                               "�� �� ������ ���� ���� ���� �ӹ��� �� �����.:0"});
        talkData.Add(30 + 1000, new string[]{ "�� ���� �Դ� ����ī ?\n����� ������� ���� ����� ������� �ִµ�:0",
                                               "������ �����Ǿ���, ���� �ٷ� ����Ǿ������ ����\n������� ����� ��Ź�ϸ�:0",
                                               "������ ���̾ƴϴ� �����ϱ� �ٷ�:0"});
        talkData.Add(31 + 1000, new string[]{ "������ ���ƿԱ��� ! ����ī !\n���� ����� ����� ���� �����..:0",
                                               "�� ���� �ϴٺ��� ��������̾� . .\n��¶�� ����ߴ�! ����ī ǫ ������ ��:0"});



        //Portrait
        portraitData.Add(1000 + 0, portraitArr[0]); // QuestNPC
        portraitData.Add(2000 + 0, portraitArr[1]); // StoreNPC
    }
    
    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                return GetTalk(id - id % 100, talkIndex); // Get First Talk
            }
            else
            {
                return GetTalk(id - id % 10, talkIndex); // Get First Quest Talk
            }
        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
