using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ����Ʈ ���� �Ŵ��� �Ÿ����� ǥ���ϱ����� Main���� �Ŵ����ʹ� ������ ���.
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
                    if (!audioSources[j].isPlaying) // �ش��ε����� ������ҽ��� �������̾ƴ϶��.
                    {
                        audioSources[j].clip = sfxList[i];
                        audioSources[j].Play();
                        return;
                    }
                }
                Debug.Log("��� ���� AudioSource�� ������Դϴ�.");
                return;
            }
        }
        Debug.Log(sfxName + "�� clip�� ��ϵǾ����� �ʽ��ϴ�.");
    }

}
