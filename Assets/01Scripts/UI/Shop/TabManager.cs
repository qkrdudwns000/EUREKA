using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject[] Tab;
    public Image[] TabBtnImage;
    public Sprite[] IdleSprite, SelectSprite; // ��ư Ȱ�� ��Ȱ��ȭ ��ư.

    public void Start() => TabClick(0);
    public void TabClick(int n)
    {
        for(int i = 0; i < Tab.Length; i++)
        {
            Tab[i].SetActive(i == n); // i == n �϶��� Ȱ��ȭ.
            TabBtnImage[i].sprite = i == n ? SelectSprite[i] : IdleSprite[i];
        }
    }
}
