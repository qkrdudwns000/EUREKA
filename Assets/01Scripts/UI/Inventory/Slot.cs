using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler,
    IDragHandler, IEndDragHandler, IDropHandler
{
    private Vector3 originPos;

    public Item item; //ȹ���� ������
    public int itemCount; // ȹ���� �������� ����
    public Image itemImage; // �������̹���

    //�ʿ��� ���۳�Ʈ
    [SerializeField]
    private TMPro.TMP_Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private void Start()
    {
        originPos = transform.position;
    }

    // �̹��� ���� ����
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
                if(item.itemType == Item.ItemType.Equipment)
                {
                    //����.
                }
                else
                {
                    //�Ҹ�.
                    Debug.Log(item.itemName + "�� ����߽��ϴ�.");
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.inst.dragSlot = this;
            DragSlot.inst.DragSetImage(itemImage);

            DragSlot.inst.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.inst.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.inst.SetColor(0);
        DragSlot.inst.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.inst.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.inst.dragSlot.item, DragSlot.inst.dragSlot.itemCount);

        if(_tempItem != null)
        {
            DragSlot.inst.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.inst.dragSlot.ClearSlot();
        }
    }
}
