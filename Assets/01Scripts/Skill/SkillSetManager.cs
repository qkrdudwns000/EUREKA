using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetManager : MonoBehaviour
{
    public static bool isSkillSetting = false;

    [SerializeField]
    private GameObject go_SkillSetBase;
    [SerializeField]
    private SkillSlotToolTip skillSlotTollTip;

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
    private void CloseSkillSet()
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
}
