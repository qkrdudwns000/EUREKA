using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSetManager : MonoBehaviour
{
    public static bool isSkillSetting = false;
    public static bool isSkill = false;

    
    public SkillSlot[] skillSlots;
    [SerializeField]
    private GameObject go_SkillSetBase;
    [SerializeField]
    private GameObject go_studySkillPopup;
    [SerializeField]
    private GameObject go_dontEnoughStudySkill;
    [SerializeField]
    private GameObject go_BaseUI;
    [SerializeField]
    private TMPro.TMP_Text text_dontEnoughPopup;
    [SerializeField]
    private SkillSlotToolTip skillSlotTollTip;

    private int skillID;
    public const int Skill1 = 0, Skill2 = 1, Skill3 = 2, Skill4 = 3;

    public SkillSlot[] GetSkillSlots() { return skillSlots; }
    [SerializeField] private Skill[] skills;
    public void LoadToSkillset(int _arrayNum, int _skillID, bool _isActivity)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].skillID == _skillID)
            {
                if(_isActivity)
                skillSlots[_arrayNum].SkillActivity();
            }
        }
    }

    private void Start()
    {
        isSkill = false;
    }

    public void TryOpenSkillSet()
    {
        if (!isSkillSetting)
            OpenSkillSet();
        else
            CloseSkillSet();        
    }

    private void OpenSkillSet()
    {
        SoundManager.inst.SFXPlay("MenuPopup");
        isSkillSetting = true;
        go_SkillSetBase.SetActive(true);
        go_BaseUI.transform.SetSiblingIndex(7);
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
        SoundManager.inst.SFXPlay("MainConfirm");
        go_studySkillPopup.SetActive(true);
        skillID = _skillID;
    }
    public void CloseStudySkillPopup()
    {
        SoundManager.inst.SFXPlay("MainCancel");
        go_studySkillPopup.SetActive(false);
    }
    public void CloseDontEnoughSkillPopup()
    {
        SoundManager.inst.SFXPlay("MainCancel");
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
            SoundManager.inst.SFXPlay("MainOK");
            go_studySkillPopup.SetActive(false);
            GameManager.Inst.SkillPoint -= skillSlots[_skill].skill.requireSkillPoint;
            skillSlots[_skill].SkillActivity();
        }
        else
        {
            SoundManager.inst.SFXPlay("MainCancelPopup");
            go_studySkillPopup.SetActive(false);
            text_dontEnoughPopup.text = "스킬포인트가 충분치 않습니다.";
            go_dontEnoughStudySkill.SetActive(true);
        }
    }
    public void DonEnoughStudyPrevSKill()
    {
        SoundManager.inst.SFXPlay("MainCancelPopup");
        text_dontEnoughPopup.text = "선행스킬 습득이 필요합니다.";
        go_dontEnoughStudySkill.SetActive(true);
    }
}
