using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSkillSlot : MonoBehaviour
{
    static public DragSkillSlot inst;

    public SkillSlot dragSkillSlot;
    [SerializeField]
    private Image imageSkill;

    private void Start()
    {
        inst = this;
    }
    public void DragSetImage(Image _skillImage)
    {
        imageSkill.sprite = _skillImage.sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = imageSkill.color;
        color.a = _alpha;
        imageSkill.color = color;
    }
}
