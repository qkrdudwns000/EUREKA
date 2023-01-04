using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject go_SkillSlotToolTipBase;

    [SerializeField] private TMPro.TMP_Text txt_SkillName;
    [SerializeField] private TMPro.TMP_Text txt_SkillUseSp;
    [SerializeField] private TMPro.TMP_Text txt_SkillCoolTime;
    [SerializeField] private TMPro.TMP_Text txt_SkillDesc;
    [SerializeField] private TMPro.TMP_Text txt_SkillPoint;
    [SerializeField] private Image img_SkillImage;


    public void ShowToolTip(Skill _skill, Vector3 _pos)
    {
        go_SkillSlotToolTipBase.SetActive(true);
        _pos += new Vector3(go_SkillSlotToolTipBase.GetComponent<RectTransform>().rect.width * 0.5f,
            -10.0f, 0.0f);
        go_SkillSlotToolTipBase.transform.position = _pos;

        go_SkillSlotToolTipBase.SetActive(true);
        txt_SkillName.text = _skill.skillName;
        txt_SkillUseSp.text = _skill.useSpAmount.ToString();
        txt_SkillCoolTime.text = _skill.skillCoolTime.ToString();
        txt_SkillDesc.text = _skill.skillDesc;
        txt_SkillPoint.text = _skill.requireSkillPoint.ToString();
        img_SkillImage.sprite = _skill.skillImage;
    }
    public void HideToolTip()
    {
        go_SkillSlotToolTipBase.SetActive(false);
    }
}
