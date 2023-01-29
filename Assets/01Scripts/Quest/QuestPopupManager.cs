using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopupManager : MonoBehaviour
{
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Image panel;


    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = panel.GetComponent<Animator>();
    }

    public void OnOpen()
    {
        openButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
        myAnim.SetTrigger("OpenPopup");
    }

    public void OnClose()
    {
        openButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(false);
        myAnim.SetTrigger("ClosePopup");
    }
}
