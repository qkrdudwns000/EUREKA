using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_MenuPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TryOpenMenu();
    }

    public void TryOpenMenu()
    {
        if (!GameManager.isPause)
            OpenMenu();
        else
            CloseMenu();
    }
    public void OpenMenu()
    {
        GameManager.isPause = true;
        go_MenuPanel.SetActive(true);
    }
    public void CloseMenu()
    {
        GameManager.isPause = false;
        go_MenuPanel.SetActive(false);
        
    }

}
