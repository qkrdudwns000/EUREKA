using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSettingBtnType : MonoBehaviour
{
    public SetBTNtype currentType;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;

    private string sceneName = "MainScene";
    [SerializeField] private PauseMenu thePaseMenu;

    bool isSound;
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case SetBTNtype.SAVE:
                thePaseMenu.CloseMenu();
                SaveNLoad.inst.ClearData();
                SaveNLoad.inst.SaveData();
                break;
            case SetBTNtype.LOAD:
                thePaseMenu.CloseMenu();
                LoadingSceneController.LoadScend(sceneName, true);
                break;
            case SetBTNtype.OPTION:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case SetBTNtype.MAINBACK:
                thePaseMenu.CloseMenu();
                break;
            case SetBTNtype.SOUND:
                Debug.Log("SOUND");
                break;
            case SetBTNtype.BACK:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case SetBTNtype.QUIT:
                Application.Quit();
                Debug.Log("����");
                break;
        }
    }
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
