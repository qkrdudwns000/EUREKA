using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst = null;

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
    public GameObject scanObject;
    static public bool isAction;
    public int talkIndex;


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
        Gold = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        GoldManager();
        SkillPointManager();
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }
    private void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
        if(isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;

            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;
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
