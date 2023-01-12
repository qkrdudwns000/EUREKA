using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultBgController : MonoBehaviour
{
    public Sprite[] titlePortraits; // 0 == Victory , 1 == Defeat
    public Image resultTitleImage;
    public Image resultIngredientItem;
    public TMPro.TMP_Text text_Gold;
    public TMPro.TMP_Text text_Experience;
    [SerializeField] private GameObject go_ResultScreen;

    const int VICTORY = 0, DEFEAT = 1;


    public void OpenResult(int _gold, int _experience, Item _ingredientItem, int _result = 0)
    {
        go_ResultScreen.SetActive(true);
        if(_result == VICTORY)
        {
            resultTitleImage.sprite = titlePortraits[VICTORY];
            text_Gold.text = _gold.ToString();
            text_Experience.text = _experience.ToString();
            resultIngredientItem.sprite = _ingredientItem.itemImage;
        }
        else
        {
            resultTitleImage.sprite = titlePortraits[DEFEAT];
            text_Gold.text = "0";
            text_Experience.text = "0";
        }
    }
    public void CloseResult()
    {
        go_ResultScreen.SetActive(false);
    }
}
