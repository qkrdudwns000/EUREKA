using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject[] Tab;
    public Image[] TabBtnImage;
    public Sprite[] IdleSprite, SelectSprite; // 버튼 활성 비활성화 버튼.

    public void Start() => TabClick(0);
    public void TabClick(int n)
    {
        for(int i = 0; i < Tab.Length; i++)
        {
            Tab[i].SetActive(i == n); // i == n 일때만 활성화.
            TabBtnImage[i].sprite = i == n ? SelectSprite[i] : IdleSprite[i];
        }
    }
}
