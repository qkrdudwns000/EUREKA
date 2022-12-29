using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text levelText;
    [SerializeField] private Image experienceBar;
    [SerializeField] private PlayerController thePlayer;
    [SerializeField] private GameObject levelUpEffect;


    private LevelSystem levelSystem;

    private void Awake()
    {

    }
    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBar.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = levelNumber.ToString();
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        // 레벨 업데이트
        SetLevelNumber(levelSystem.GetLevelNumber());
        Instantiate(levelUpEffect, thePlayer.transform.position, Quaternion.identity);
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        // 경험치바 업데이트
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }

    public void Experience60Btn()
    {
        levelSystem.AddExperience(60);
    }
    public void Experience300Btn()
    {
        levelSystem.AddExperience(300);
    }
}
