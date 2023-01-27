using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public CanvasGroup mainGroup;

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            PressAnyButton();
        }
    }
    private void PressAnyButton()
    {
        mainGroup.alpha = 1;
        mainGroup.interactable = true;
        mainGroup.blocksRaycasts = true;
        SoundManager.inst.SFXPlay("TitleConfirm");

        this.gameObject.SetActive(false);
    }
}
