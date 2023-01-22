using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//보스 이펙트 사운드 매니저 거리감을 표현하기위해 Main사운드 매니저와는 별개로 사용.
public class BossSM : MonoBehaviour
{
    public static BossSM inst;
    public AudioClip[] sfxList;
    public AudioSource[] audioSources;
    private void Awake()
    {
        inst = this;
    }


    public void SFXPlay(string sfxName)
    {
        for (int i = 0; i < sfxList.Length; i++)
        {
            if (sfxList[i].name == sfxName)
            {
                for(int j = 0; j < audioSources.Length; j++)
                {
                    if (!audioSources[j].isPlaying) // 해당인덱스의 오디오소스가 실행중이아니라면.
                    {
                        audioSources[j].clip = sfxList[i];
                        audioSources[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource를 사용중입니다.");
                return;
            }
        }
        Debug.Log(sfxName + "이 clip에 등록되어있지 않습니다.");
    }

}
