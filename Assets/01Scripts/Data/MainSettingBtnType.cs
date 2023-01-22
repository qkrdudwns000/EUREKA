using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainSettingBtnType : MonoBehaviour, IPointerEnterHandler
{
    public SetBTNtype currentType;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    [SerializeField]private GameObject go_SoundMenu;

    private string sceneName = "MainScene";
    [SerializeField] private PauseMenu thePaseMenu;
    public Slider BgmSlider;

    bool isSound;
    private void Start()
    {
        
    }
    public void OnBtnClick()
    {
        SoundManager.inst.SFXPlay("TitleConfirm");
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
                OpenSoundMenu();
                break;
            case SetBTNtype.BACK:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case SetBTNtype.QUIT:
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
        go_SoundMenu.SetActive(true);
        CanvasGroupOff(mainGroup);
        CanvasGroupOff(optionGroup);
    }
    public void CloseSoundMenu()
    {
        SoundManager.inst.SFXPlay("MainCancel");
        go_SoundMenu.SetActive(false);
        CanvasGroupOn(optionGroup);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.inst.SFXPlay("TitleDrag");
    }
}
