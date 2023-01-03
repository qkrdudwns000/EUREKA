using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "New SKill/skill")]
public class Skill : ScriptableObject
{
    public int skillID;
    public string skillName;
    public Sprite skillImage;
    public float skillCollTime;
    public int requireSkillPoint;
    public float useSpAmount;


    public Skill prevSkill;
    public Skill nextSkill;

    [TextArea]
    public string skillDesc;

}
