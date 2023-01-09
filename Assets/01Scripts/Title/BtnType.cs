using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnType : MonoBehaviour
{
    public BTNtype currentType;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    bool isSound;
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNtype.NEW:
                LoadingSceneController.LoadScend("MainScene");
                break;
            case BTNtype.CONTINUE:
                Debug.Log("����ϱ�");
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
