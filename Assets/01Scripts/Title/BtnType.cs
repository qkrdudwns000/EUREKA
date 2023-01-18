using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BtnType : MonoBehaviour
{
    public BTNtype currentType;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    private string sceneName = "MainScene";
    [SerializeField] private Title theTitle;
    [SerializeField] private SaveNLoad theSaveNLoad;

    bool isSound;

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNtype.NEW:
                LoadingSceneController.LoadScend(sceneName, false);
                break;
            case BTNtype.CONTINUE:
                LoadingSceneController.LoadScend(sceneName, true);
                break;
            case BTNtype.OPTION:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BTNtype.SOUND:
                Debug.Log("SOUND");
                break;
            case BTNtype.BACK:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case BTNtype.QUIT:
                Application.Quit();
                Debug.Log("Á¾·á");
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
