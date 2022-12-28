using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    private int level;
    private int experience;
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
        if(experience >= experienceToNextLevel)
        {
            //·¹º§¾÷
            level++;
            experience -= experienceToNextLevel;
        }
    }
}
