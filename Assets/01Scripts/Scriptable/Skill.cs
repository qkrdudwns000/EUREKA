using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "New SKill/skill")]
public class Skill : ScriptableObject
{
    public int skillID;
    public string skillName;
    public string animeName;
    public Sprite skillImage;
    public float skillCoolTime;
    public float skillValue;
    public int requireSkillPoint;
    public float useSpAmount;


    public Skill prevSkill;
    public Skill nextSkill;

    [TextArea]
    public string skillDesc;

}
