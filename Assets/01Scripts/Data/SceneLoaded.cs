using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaded : MonoBehaviour
{
    private static SceneLoaded inst = null;
    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public static SceneLoaded Inst
    {
        get
        {
            if (inst == null)
                return null;

            return inst;
        }
    }

    public int _gold;
    public int _level;
    public int _experience;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        SceneData();
    }

    private void SceneData()
    {
        GameManager.Inst.Gold = _gold;
        GameManager.Inst.levelSystem.level = _level;
        GameManager.Inst.levelSystem.experience = _experience;
    }
}
