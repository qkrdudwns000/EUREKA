using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveDataALL
{
    public Vector3 _playerPos;
    public Vector3 _playerRot;

    public int _gold;
    public int _level;
    public int _experience;
    public int _skillPoint;

    public int _questId;
    public int _questActionIndex;
    public int _questPopupIndex;
    public bool _questComplete;

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

public class SaveNLoad : MonoBehaviour
{
    [SerializeField]
    private SaveDataALL saveData = new SaveDataALL();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private PlayerController thePlayer;
    private Inventory theInven;
    private SkillSetManager theSkillSetManager;
    private QuickSlotController theQuickSlotController;
    private QuestManager theQuestManager;

    public static SaveNLoad inst = null;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY)) //경로에 존재하는지 bool값반환
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }


    public void SaveData()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theInven = FindObjectOfType<Inventory>();
        theSkillSetManager = FindObjectOfType<SkillSetManager>();
        theQuickSlotController = FindObjectOfType<QuickSlotController>();
        theQuestManager = FindObjectOfType<QuestManager>();

        saveData._playerPos = thePlayer.transform.position;
        saveData._playerRot = thePlayer.transform.eulerAngles;
        saveData._gold = GameManager.Inst.Gold;
        saveData._level = GameManager.Inst.levelSystem.GetLevelNumber();
        saveData._experience = GameManager.Inst.levelSystem.experience;
        saveData._skillPoint = GameManager.Inst.SkillPoint;
        saveData._questId = theQuestManager.questId;
        saveData._questActionIndex = theQuestManager.questActionIndex;
        saveData._questPopupIndex = theQuestManager.questPopupIndex;
        saveData._questComplete = theQuestManager.questComplete;

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
        for (int i = 0; i < skillSlots.Length; i++)
        {
            if (skillSlots[i].skill != null)
            {
                saveData.skillSetArrayNumber.Add(i);
                saveData.skillSetSkillID.Add(skillSlots[i].skill.skillID);
                saveData.skillSetSkillActivity.Add(skillSlots[i].activitySkill);
            }
        }
        QuickSlot[] quickSlots = theQuickSlotController.GetQuickSlots();
        for (int i = 0; i < quickSlots.Length; i++)
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

        string json = JsonUtility.ToJson(saveData);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Open(SAVE_DATA_DIRECTORY + SAVE_FILENAME, FileMode.Create);
        //bf.Serialize(file, json);
        //file.Close();
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        
        Debug.Log("저장 완료");
        Debug.Log(json);
    }
    public void LoadDatasCo()
    {
        StopAllCoroutines();
        StartCoroutine(LoadDataCo());
    }
    IEnumerator LoadDataCo()
    {
        yield return null;
        LoadData();
        if(GameObject.Find("Title") != null)
        {
            GameObject title = GameObject.Find("Title");
            title.gameObject.SetActive(false);
        }
        
    }
    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveDataALL>(loadJson);

            thePlayer = FindObjectOfType<PlayerController>();
            theInven = FindObjectOfType<Inventory>();
            theSkillSetManager = FindObjectOfType<SkillSetManager>();
            theQuickSlotController = FindObjectOfType<QuickSlotController>();
            theQuestManager = FindObjectOfType<QuestManager>();

            thePlayer.transform.position = saveData._playerPos;
            thePlayer.transform.eulerAngles = saveData._playerRot;
            GameManager.Inst.Gold = saveData._gold;
            GameManager.Inst.levelSystem.level = saveData._level;
            GameManager.Inst.levelSystem.experience = saveData._experience;
            GameManager.Inst.SkillPoint = saveData._skillPoint;
            theQuestManager.questId = saveData._questId;
            theQuestManager.questActionIndex = saveData._questActionIndex;
            theQuestManager.questPopupIndex = saveData._questPopupIndex;
            theQuestManager.questComplete = saveData._questComplete;

            GameManager.Inst.SetLevelSystem(GameManager.Inst.levelSystem);
                     
            //SceneLoaded.Inst._gold = saveData._gold;
            //SceneLoaded.Inst._level = saveData._level;
            //SceneLoaded.Inst._experience = saveData._experience;
            //SceneLoaded.Inst._skillPoint = saveData._skillPoint;
            //SceneLoaded.Inst._questId = saveData._questId;
            //SceneLoaded.Inst._questActionIndex = saveData._questActionIndex;
            //SceneLoaded.Inst._questPopupIndex = saveData._questPopupIndex;
            //SceneLoaded.Inst._questComplete = saveData._questComplete;

            for (int i = 0; i < saveData.invenItemName.Count; i++)
            {
                theInven.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemName[i],
                    saveData.invenItemNumber[i], saveData.invenItemIsEquip[i]);
            }
            theInven.CheckEquip();
            for (int i = 0; i < saveData.skillSetSkillID.Count; i++)
            {
                theSkillSetManager.LoadToSkillset(saveData.skillSetArrayNumber[i], saveData.skillSetSkillID[i], saveData.skillSetSkillActivity[i]);
            }
            for (int i = 0; i < saveData.quickUsedItemName.Count; i++)
            {
                theQuickSlotController.LoadToQuickItem(saveData.quickItemArrayNumber[i], saveData.quickUsedItemName[i],
                    saveData.quickUsedItemNumber[i]);
            }
            for (int i = 0; i < saveData.quickSkillID.Count; i++)
            {
                theQuickSlotController.LoadToQuickSkill(saveData.quickSkillArrayNumber[i], saveData.quickSkillID[i]);
            }

            Debug.Log("로드 완료");
        }
        else
            Debug.Log("로드할 파일이 없습니다.");

        if(GameObject.Find("Title") != null)
        {
            GameObject title = GameObject.Find("Title");
            title.SetActive(false);
        }

    }
    public void ClearData()
    {
        saveData.invenArrayNumber.Clear();
        saveData.invenItemName.Clear();
        saveData.invenItemNumber.Clear();
        saveData.invenItemIsEquip.Clear();

        saveData.skillSetArrayNumber.Clear();
        saveData.skillSetSkillID.Clear();
        saveData.skillSetSkillActivity.Clear();

        saveData.quickItemArrayNumber.Clear();
        saveData.quickUsedItemName.Clear();
        saveData.quickUsedItemNumber.Clear();

        saveData.quickSkillArrayNumber.Clear();
        saveData.quickSkillID.Clear();
    }
}
