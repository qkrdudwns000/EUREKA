using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class SaveData
{
    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
    public List<bool> invenItemIsEquip = new List<bool>();
}
public class SceneLoaded : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    private Inventory theInven;

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
        theInven = FindObjectOfType<Inventory>();
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        if (!scene.name.Equals("LoadingScene")) { SceneData(); }
    }

    private void SceneData()
    {
        GameManager.Inst.Gold = _gold;
        GameManager.Inst.levelSystem.level = _level;
        GameManager.Inst.levelSystem.experience = _experience;
        LoadData();
    }

    public void SaveData()
    {
        Slot[] slots = theInven.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemNumber.Add(slots[i].itemCount);
                saveData.invenItemIsEquip.Add(slots[i].isEquip);
            }
        }
    }
    private void LoadData()
    {
        for(int i = 0; i < saveData.invenItemName.Count; i++)
        {
            theInven.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemName[i], saveData.invenItemNumber[i], saveData.invenItemIsEquip[i]);
        }
    }
    
}
