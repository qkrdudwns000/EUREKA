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
        talkData.Add(1000, new string[] { "�ȳ�:0", "���� ������:0" });
        talkData.Add(2000, new string[] { "�� ~ ! �������� ������ ���� ����� ~ !:0" });

        portraitData.Add(1000 + 0, portraitArr[0]); // QuestNPC
        portraitData.Add(2000 + 0, portraitArr[1]); // StoreNPC
    }
    
    public string GetTalk(int id, int talkIndex)
    {
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
