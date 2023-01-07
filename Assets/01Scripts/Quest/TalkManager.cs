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
        talkData.Add(1000, new string[] { "안녕:0", "좋은 날씨야:0" });
        talkData.Add(2000, new string[] { "여 ~ ! 좋은물건 많으니 보고 가라고 ~ !:0" });

        //QuestTalk Data
        talkData.Add(10 + 1000, new string[] { "오! 유레카구나\n헌터에 지원했다는 소문을 들었다.:0",
                                               "하지만 무기도 없이 토벌에 나간다는 것은 말이 안되지\n앞의 상인에게서 무기와 방패를 사보려구나:0"});
        talkData.Add(11 + 2000, new string[] { "포션 ~ 무기 ~ 방패 없는게 없습니다요~:0",
                                               "오 새로 지원한 헌터신가 보군요 ?? \n싼 값에 드릴테니 보고 가십시오 ~:0"});
        talkData.Add(20 + 1000, new string[] { "오 ~ 무기를 차고나니 제법 헌터 다워 졌구나\n첫 토벌을 진행해보자 그러면 성문입구로 가면 다음 지역으로 이동할 수 있다.:0",
                                               "북쪽폐허의 그 녀석을 토벌하고 와주길 바래\n부디 승리하고 돌아오길 . .:0"});



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
