using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BtnType : MonoBehaviour, IPointerEnterHandler
{
    public BTNtype currentType;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    private string sceneName = "MainScene";
    [SerializeField] private Title theTitle;
    [SerializeField] private SaveNLoad theSaveNLoad;
    [SerializeField] private GameObject go_SoundPanel;
    [SerializeField] private GameObject go_NoDataPopup;

    bool isSound;

    public void OnBtnClick()
    {
        SoundManager.inst.SFXPlay("TitleConfirm");
        switch (currentType)
        {
            case BTNtype.NEW:
                LoadingSceneController.LoadScend(sceneName, false);
                break;
            case BTNtype.CONTINUE:
                if (SaveNLoad.inst.DataConfirm())
                    LoadingSceneController.LoadScend(sceneName, true);
                else
                    go_NoDataPopup.SetActive(true);

                break;
            case BTNtype.OPTION:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BTNtype.SOUND:
                OpenSoundMenu();
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
    public void OpenSoundMenu()
    {
        go_SoundPanel.SetActive(true);
    }
    public void CloseSoundMenu()
    {
        go_SoundPanel.SetActive(false);
    }

    public void CloseNoDataPopup()
    {
        go_NoDataPopup.SetActive(false);
        SoundManager.inst.SFXPlay("MainCancelPopup");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.inst.SFXPlay("TitleDrag");
    }
}
