using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler,
    IDragHandler, IEndDragHandler, IDropHandler
{
    private Vector3 originPos;

    public Item item; //ȹ���� ������
    public int itemCount; // ȹ���� �������� ����
    public bool isEquip = false;
    public Image itemImage; // �������̹���

    //�ʿ��� ���۳�Ʈ
    [SerializeField]
    private TMPro.TMP_Text text_Count;
    [SerializeField]
    private GameObject go_CountImage; // ������ ī���� �̹���
    public GameObject go_EquipImage; // ������ ���� �̹���
    [SerializeField]
    private Equipment theWeaponEquip;
    [SerializeField]
    private Equipment theShieldEquip;
    private Inventory theInven;

    private void Start()
    {
        originPos = transform.position;
        theInven = FindObjectOfType<Inventory>();
    }

    // �̹��� ���� ����
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    // ������ ȹ���
    public void AddItem(Item _item, int _count = 1, bool _isEquip = false)
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
        if (_isEquip)
        {
            go_EquipImage.SetActive(true);
            isEquip = true;
        }
        else
        {
            go_EquipImage.SetActive(false);
            isEquip = false;
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
        isEquip = false;
        go_EquipImage.SetActive(false);
        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (!Shop.isShopping) // ���� UI �����������.
            {
                if (item != null)
                {
                    if (item.itemType == Item.ItemType.Equipment)
                    {
                        isEquip = true;
                        //����.
                        if (item.weaponType == Item.WeaponType.Weapon)
                        {
                            theWeaponEquip.WeaponEquip(item, this);
                        }
                        else if (item.weaponType == Item.WeaponType.Shield)
                        {
                            theShieldEquip.ShieldEquip(item, this);
                        }
                    }
                    else
                    {
                        //�Ҹ�.
                        Debug.Log(item.itemName + "�� ����߽��ϴ�.");
                        SetSlotCount(-1);
                    }
                }
            }
            else // ���� ui �����������
            {
                if (item != null && !isEquip)
                {
                    StoreManager.Inst.SellPopupOpen(this);
                }
            }
        }
    }
    public void SellItem()
    {
        if (item.itemType == Item.ItemType.Equipment)
        {
            if (!isEquip)
            {
                GameManager.Inst.Gold += item.itemPrice / 2;
                ClearSlot();
            }
        }
        else
        {
            GameManager.Inst.Gold += item.itemPrice / 2;
            SetSlotCount(-1);
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
        bool _tempisEquip = isEquip;

        AddItem(DragSlot.inst.dragSlot.item, DragSlot.inst.dragSlot.itemCount, DragSlot.inst.dragSlot.isEquip);

        if(_tempItem != null)
        {
            DragSlot.inst.dragSlot.AddItem(_tempItem, _tempItemCount, _tempisEquip);
        }
        else
        {
            DragSlot.inst.dragSlot.ClearSlot();
        }
    }
    // ���콺�� ���Կ� �� �� �ߵ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            theInven.ShowToolTip(item);
        }
    }

    // ���콺�� ���Կ��� ���� �� �ߵ�.
    public void OnPointerExit(PointerEventData eventData)
    {
        theInven.HideToolTip();
    }
}
