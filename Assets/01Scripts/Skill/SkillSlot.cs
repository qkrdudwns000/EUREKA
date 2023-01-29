using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    , IPointerClickHandler
{
    public Skill skill;
    public Image skillImage;
    public Image disableSkillImage;
    public int requireSkillPoint;
    public bool activitySkill = false;

    [SerializeField]
    private SkillSlot prevSkillSlot;

    private SkillSetManager theSkillSetManager;

    private void Start()
    {
        theSkillSetManager = FindObjectOfType<SkillSetManager>();
    }
    private void SetColorDisableImage()
    {
        Color color = disableSkillImage.color;
        color.a = 0;
        disableSkillImage.color = color;
    }
    public void SkillActivity()
    {
        activitySkill = true;
        SetColorDisableImage();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left && !activitySkill)
        {
            if(prevSkillSlot != null)
            {
                if(prevSkillSlot.activitySkill)
                {
                    theSkillSetManager.OpenStudySkillPopup(skill.skillID, skill.requireSkillPoint);
                }
                else
                {
                    theSkillSetManager.DonEnoughStudyPrevSKill();
                }
            }
            else
            {
                theSkillSetManager.OpenStudySkillPopup(skill.skillID, skill.requireSkillPoint);
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (skill != null && activitySkill)
        {
            DragSkillSlot.inst.dragSkillSlot = this;
            DragSkillSlot.inst.DragSetImage(skillImage);
            DragSkillSlot.inst.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(skill != null)
            DragSkillSlot.inst.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSkillSlot.inst.SetColor(0);
        DragSkillSlot.inst.dragSkillSlot = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(skill != null)
            theSkillSetManager.ShowToolTip(skill, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theSkillSetManager.HideToolTip();
    }
}

