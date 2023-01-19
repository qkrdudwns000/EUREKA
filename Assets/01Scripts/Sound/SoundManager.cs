using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource bgmSound;
    public AudioClip[] sfxList;
    public AudioClip[] bgmList;
    public static SoundManager inst;
    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(inst);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            Destroy(gameObject);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i = 0; i<bgmList.Length; i++)
        {
            if(arg0.name == bgmList[i].name)
            {
                BGMPlay(bgmList[i]);
            }
        }
    }
    public void BGMSoundVolume(float v)
    {
        mixer.SetFloat("BGMSound", Mathf.Log10(v) * 20);
    }
    public void SFXSoundVolume(float v)
    {
        mixer.SetFloat("SFXSound", Mathf.Log10(v) * 20);
    }

    public void SFXPlay(string sfxName)
    {
        for(int i = 0; i < sfxList.Length; i++)
        {
            if (sfxList[i].name == sfxName)
            {
                GameObject go = new GameObject(sfxName + "Sound");
                AudioSource audioSource = go.AddComponent<AudioSource>();
                audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
                audioSource.clip = sfxList[i];
                audioSource.Play();

                Destroy(go, sfxList[i].length); // clip의 사운드끝나면 파괴.
            }
            else
            {
                Debug.Log("해당 AudioClip이 List에 없습니다.");
            }
        }
        
    }

    public void BGMPlay(AudioClip clip)
    {
        bgmSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        bgmSound.clip = clip;
        bgmSound.loop = true;
        bgmSound.volume = 0.1f;
        bgmSound.Play();
    }
}
