using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultBgController : MonoBehaviour
{
    static public bool isResulting = false;

    public Sprite[] titlePortraits; // 0 == Victory , 1 == Defeat
    public Image resultTitleImage;
    public Image resultIngredientItem;
    public TMPro.TMP_Text text_Gold;
    public TMPro.TMP_Text text_Experience;
    [SerializeField] private GameObject go_ResultScreen;
    [SerializeField] private QuestManager theQuestManager;

    const int VICTORY = 1, DEFEAT = 0;


    public void OpenResult(int _gold, int _experience, Item _ingredientItem)
    {
        isResulting = true;
        go_ResultScreen.SetActive(true);
        resultTitleImage.sprite = titlePortraits[VICTORY];
        text_Gold.text = _gold.ToString();
        text_Experience.text = _experience.ToString();
        resultIngredientItem.sprite = _ingredientItem.itemImage;
        SetColor(VICTORY);

    }
    public void OpenResult()
    {
        isResulting = true;

        go_ResultScreen.SetActive(true);

        resultTitleImage.sprite = titlePortraits[DEFEAT];
        text_Gold.text = "0";
        text_Experience.text = "0";
        SetColor(DEFEAT);
    }
    public void CloseResult()
    {
        go_ResultScreen.SetActive(false);
    }

    private void SetColor(int _alpha = 0)
    {
        Color color = resultIngredientItem.color;
        color.a = _alpha;
        resultIngredientItem.color = color;
    }

    public void GoToMainScene()
    {
        SceneLoaded.Inst._gold = GameManager.Inst.Gold;
        SceneLoaded.Inst._level = GameManager.Inst.levelSystem.GetLevelNumber();
        SceneLoaded.Inst._experience = GameManager.Inst.levelSystem.experience;
        SceneLoaded.Inst._skillPoint = GameManager.Inst.SkillPoint;
        SceneLoaded.Inst._questId = theQuestManager.questId;
        SceneLoaded.Inst._questActionIndex = theQuestManager.questActionIndex;
        SceneLoaded.Inst._questPopupIndex = theQuestManager.questPopupIndex;
        SceneLoaded.Inst._questComplete = theQuestManager.questComplete;
        SceneLoaded.Inst.SaveData();

        isResulting = false;


        LoadingSceneController.LoadScend("MainScene", false);
    }
}
