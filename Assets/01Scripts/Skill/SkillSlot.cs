using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Skill skill;
    public Image skillImage;
    public int requireSkillPoint;

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (skill != null)
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

}

