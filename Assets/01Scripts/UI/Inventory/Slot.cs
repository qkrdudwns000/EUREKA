using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item; //ȹ���� ������
    public int itemCount; // ȹ���� �������� ����
    public Image itemImage; // �������̹���

    //�ʿ��� ���۳�Ʈ
    [SerializeField]
    private TMPro.TMP_Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    // �̹��� ������ ����
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    // ������ ȹ���
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        if (item.itemType != Item.ItemType.Equipment)
        {
            text_Count.text = itemCount.ToString();
            go_CountImage.SetActive(true);
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }
    // ������ ��������
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if(itemCount <= 0)
        {
            ClearSlot();
        }
    }
    // ���� �ʱ�ȭ.
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);
        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }
}