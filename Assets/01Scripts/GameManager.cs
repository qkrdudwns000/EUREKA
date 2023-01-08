using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst = null;
    public QuestManager questManager;

    [SerializeField] int _gold = 0;
    [SerializeField] int _skillPoint = 0;

    [SerializeField]
    private TMPro.TMP_Text text_gold;
    [SerializeField]
    private TMPro.TMP_Text text_skillPoint;

    [SerializeField] private TMPro.TMP_Text levelText;
    [SerializeField] private Image experienceBar;
    [SerializeField] private PlayerController thePlayer;
    [SerializeField] private TalkManager talkManager;
    [SerializeField] private GameObject levelUpEffect;

    private LevelSystem levelSystem;

    [SerializeField] GameObject talkPanel;
    [SerializeField] Image portraitImg;
    [SerializeField] private TMPro.TMP_Text talkText;
    [SerializeField] private TMPro.TMP_Text nameText;
    private float textSpeed = 0.01f;
    private bool canSkip = false;
    public GameObject scanObject;
    static public bool isAction;
    public int talkIndex;

    private SpringArm theCam;


    public int Gold
    {
        get => _gold;
        set => _gold = value;
    }
    public int SkillPoint
    {
        get => _skillPoint;
        set => _skillPoint = value;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        theCam = FindObjectOfType<SpringArm>();
        Gold = 10000;
        canSkip = false;
    }

    // Update is called once per frame
    void Update()
    {
        GoldManager();
        SkillPointManager();
       //Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        if (!canSkip)
        {
            theCam.CameraOriginSetting();
            if(!theCam.isTargetting)
                theCam.CameraTargeting(scanObject.transform);

            Talk(objData.id, objData.isNpc);
        }

        talkPanel.SetActive(isAction);
    }
    private void Talk(int id, bool isNpc)
    {
        //Set Talk Data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        //End Talk
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            theCam.CameraTargeting(scanObject.transform);
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        //Continue Talk
        if(isNpc)
        {
            StopAllCoroutines();
            StartCoroutine(SaySomething(talkData.Split(':')[0]));
            nameText.text = scanObject.GetComponent<ObjData>().NpcName;
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(SaySomething(talkData));

            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;
    }
    IEnumerator SaySomething(string text)
    {
        canSkip = true;
        talkText.text = "";
        bool t_monster = false, t_white = false, t_yellow = false, t_size = false, t_orgsize = false;
        bool t_ignore = false; // 특수문자를 생략하기위한 bool값.
        for (int i = 0; i < text.Length; i++)
        {
            switch (text[i])
            {
                case 'ⓦ': t_monster = false; t_white = true; t_yellow = false; t_size = false; t_orgsize = false; t_ignore = true; break;
                case 'ⓜ': t_monster = true; t_white = false; t_yellow = false; t_size = false; t_orgsize = false; t_ignore = true; break;
                case 'ⓨ': t_monster = true; t_white = false; t_yellow = true; t_size = false; t_orgsize = false; t_ignore = true; break;
                case 'ⓢ': t_monster = false; t_white = false; t_yellow = false; t_size = true; t_orgsize = false; t_ignore = true; break;
                case 'ⓞ': t_monster = false; t_white = false; t_yellow = false; t_size = false; t_orgsize = true; t_ignore = true; break;
            }
            string t_letter = text[i].ToString();
            if (!t_ignore)
            {
                if (t_white) { t_letter = "<color=#ffffff>" + t_letter + "</color>"; }
                else if (t_monster) { t_letter = "<color=#42DEE3>" + t_letter + "</color>"; }
                else if (t_yellow) { t_letter = "<color=#FFFF00>" + t_letter + "</color>"; }
                else if (t_size) { t_letter = "<size=20>" + t_letter + "</size>"; }
                else if (t_orgsize) { t_letter = "<size=36>" + t_letter + "</size>"; }
                talkText.text += t_letter;
            }
            t_ignore = false;
            if(canSkip)
                yield return new WaitForSeconds(textSpeed);
        }
        canSkip = false;
    }

    private void GoldManager()
    {
        if(Shop.isShopping || Inventory.inventoryActivated)
        {
            text_gold.text = Gold.ToString();
        }
    }
    private void SkillPointManager()
    {
        if(SkillSetManager.isSkillSetting)
        {
            text_skillPoint.text = SkillPoint.ToString();
        }
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBar.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = levelNumber.ToString();
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        // 레벨 업데이트
        SetLevelNumber(levelSystem.GetLevelNumber());
        Instantiate(levelUpEffect, thePlayer.transform.position, Quaternion.identity);
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        // 경험치바 업데이트
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }

    public void Experience60Btn()
    {
        levelSystem.AddExperience(60);
    }
    public void Experience300Btn()
    {
        levelSystem.AddExperience(300);
    }
}
