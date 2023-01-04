using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSetManager : MonoBehaviour
{
    public static bool isSkillSetting = false;
    public static bool isSkill = false;

    [SerializeField]
    private SkillSlot[] skillSlots;
    [SerializeField]
    private GameObject go_SkillSetBase;
    [SerializeField]
    private GameObject go_studySkillPopup;
    [SerializeField]
    private GameObject go_dontEnoughStudySkill;
    [SerializeField]
    private TMPro.TMP_Text text_dontEnoughPopup;
    [SerializeField]
    private SkillSlotToolTip skillSlotTollTip;

    private int skillID;
    public const int Skill1 = 0, Skill2 = 1, Skill3 = 2, Skill4 = 3;

    // Update is called once per frame
    void Update()
    {
        if(!Shop.isShopping)
            TryOpenSkillSet();

        if (isSkillSetting && Input.GetKeyDown(KeyCode.Escape))
            CloseSkillSet();
    }

    private void TryOpenSkillSet()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!isSkillSetting)
                OpenSkillSet();
            else
                CloseSkillSet();
        }
    }

    private void OpenSkillSet()
    {
        isSkillSetting = true;
        go_SkillSetBase.SetActive(true);
    }
    public void CloseSkillSet()
    {
        isSkillSetting = false;
        go_SkillSetBase.SetActive(false);
        HideToolTip();
    }

    public void ShowToolTip(Skill _skill, Vector3 _pos)
    {
        skillSlotTollTip.ShowToolTip(_skill, _pos);
    }
    public void HideToolTip()
    {
        skillSlotTollTip.HideToolTip();
    }

    public void OpenStudySkillPopup(int _skillID)
    {
        go_studySkillPopup.SetActive(true);
        skillID = _skillID;
    }
    public void CloseStudySkillPopup()
    {
        go_studySkillPopup.SetActive(false);
    }
    public void CloseDontEnoughSkillPopup()
    {
        go_dontEnoughStudySkill.SetActive(false);
    }
    public void SatisfyRequireSkillPoint()
    {
        switch(skillID)
        {
            case 1001:
                CalcSkillPoint(Skill1);
                break;
            case 1002:
                CalcSkillPoint(Skill2);
                break;
            case 1003:
                CalcSkillPoint(Skill3);
                break;
            case 1004:
                CalcSkillPoint(Skill4);
                break;
        }
    }
    private void CalcSkillPoint(int _skill)
    {
        if (GameManager.Inst.SkillPoint - skillSlots[_skill].skill.requireSkillPoint > 0)
        {
            go_studySkillPopup.SetActive(false);
            GameManager.Inst.SkillPoint -= skillSlots[_skill].skill.requireSkillPoint;
            skillSlots[_skill].SkillActivity();
        }
        else
        {
            go_studySkillPopup.SetActive(false);
            text_dontEnoughPopup.text = "스킬포인트가 충분치 않습니다.";
            go_dontEnoughStudySkill.SetActive(true);
        }
    }
    public void DonEnoughStudyPrevSKill()
    {
        text_dontEnoughPopup.text = "선행스킬 습득이 필요합니다.";
        go_dontEnoughStudySkill.SetActive(true);
    }
}
