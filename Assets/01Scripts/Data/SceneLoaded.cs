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

    public List<int> skillSetArrayNumber = new List<int>();
    public List<int> skillSetSkillID = new List<int>();
    public List<bool> skillSetSkillActivity = new List<bool>();

    public List<int> quickItemArrayNumber = new List<int>();
    public List<int> quickSkillArrayNumber = new List<int>();
    public List<string> quickUsedItemName = new List<string>();
    public List<int> quickSkillID = new List<int>();
    public List<int> quickUsedItemNumber = new List<int>();
}
public class SceneLoaded : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    private Inventory theInven;
    private SkillSetManager theSkillSetManager;
    private QuickSlotController theQuickSlotController;

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
    public int _skillPoint;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        theInven = FindObjectOfType<Inventory>();
        theSkillSetManager = FindObjectOfType<SkillSetManager>();
        theQuickSlotController = FindObjectOfType<QuickSlotController>();

        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        if (!scene.name.Equals("LoadingScene")) { SceneData(); }
    }

    private void SceneData()
    {
        GameManager.Inst.Gold = _gold;
        GameManager.Inst.levelSystem.level = _level;
        GameManager.Inst.levelSystem.experience = _experience;
        GameManager.Inst.SkillPoint = _skillPoint;
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
        SkillSlot[] skillSlots = theSkillSetManager.GetSkillSlots();
        for(int i = 0; i < skillSlots.Length; i++)
        {
            if (skillSlots[i].skill != null)
            {
                saveData.skillSetArrayNumber.Add(i);
                saveData.skillSetSkillID.Add(skillSlots[i].skill.skillID);
                saveData.skillSetSkillActivity.Add(skillSlots[i].activitySkill);
            }
        }
        QuickSlot[] quickSlots = theQuickSlotController.GetQuickSlots();
        for(int i = 0; i < quickSlots.Length; i++)
        {
            if (quickSlots[i].quickItem != null)
            {
                saveData.quickItemArrayNumber.Add(i);
                saveData.quickUsedItemName.Add(quickSlots[i].quickItem.itemName);
                saveData.quickUsedItemNumber.Add(quickSlots[i].quickItemCount);
            }
            else if (quickSlots[i].quickSkill != null)
            {
                saveData.quickSkillArrayNumber.Add(i);
                saveData.quickSkillID.Add(quickSlots[i].quickSkill.skillID);
            }
        }
    }
        
    private void LoadData()
    {
        for(int i = 0; i < saveData.invenItemName.Count; i++)
        {
            theInven.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemName[i],
                saveData.invenItemNumber[i], saveData.invenItemIsEquip[i]);
        }
        for(int i = 0; i < saveData.skillSetSkillID.Count; i++)
        {
            theSkillSetManager.LoadToSkillset(saveData.skillSetArrayNumber[i], saveData.skillSetSkillID[i], saveData.skillSetSkillActivity[i]);
        }
        for(int i = 0; i < saveData.quickUsedItemName.Count; i++)
        {
            theQuickSlotController.LoadToQuickItem(saveData.quickItemArrayNumber[i], saveData.quickUsedItemName[i],
                saveData.quickUsedItemNumber[i]);
        }
        for(int i = 0; i < saveData.quickSkillID.Count; i++)
        {
            theQuickSlotController.LoadToQuickSkill(saveData.quickSkillArrayNumber[i], saveData.quickSkillID[i]);
        }
    }
    
}
