using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    public int level;
    public int experience;
    private int experienceToNextLevel;

    public LevelSystem()
    {
        level = 1;
        experience = 0;
        experienceToNextLevel = 100;
    }
    
    public void AddExperience(int _amount)
    {
        experience += _amount;
        while(experience >= experienceToNextLevel)
        {
            //레벨업
            level++;
            GameManager.Inst.SkillPoint++;
            experience -= experienceToNextLevel;
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public float GetExperienceNormalized()
    {
        return (float)experience / experienceToNextLevel; // 노멀라이즈화
    }
}
