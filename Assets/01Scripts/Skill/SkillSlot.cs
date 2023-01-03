using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Skill skill;
    public Image skillImage;
    public int requireSkillPoint;

}

internal interface IBeginDragHandler
{
}

internal interface IDragHandler
{
}

internal interface IEndDragHandler
{
}