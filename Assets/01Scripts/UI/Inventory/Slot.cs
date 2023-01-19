using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler,
    IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item; //획득한 아이템
    public int itemCount; // 획득한 아이템의 갯수
    public bool isEquip = false;
    public Image itemImage; // 아이템이미지

    //필요한 컴퍼넌트
    [SerializeField]
    private TMPro.TMP_Text text_Count;
    [SerializeField]
    private GameObject go_CountImage; // 아이템 카운팅 이미지
    public GameObject go_EquipImage; // 아이템 장착 이미지
    [SerializeField]
    private Equipment theWeaponEquip;
    [SerializeField]
    private Equipment theShieldEquip;
    private Inventory theInven;

    private void Start()
    {
        theInven = FindObjectOfType<Inventory>();
    }
    public void EquipCheck()
    {
       if (isEquip)
       {
           if (item.weaponType == Item.WeaponType.Weapon)
               theWeaponEquip.WeaponEquip(item, this);
           else if (item.weaponType == Item.WeaponType.Shield)
               theShieldEquip.ShieldEquip(item, this);
       }
    
    }

    // 이미지 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    // 아이템 획득시
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
    // 아이템 갯수조정
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if(itemCount <= 0)
        {
            ClearSlot();
        }
    }
    // 슬롯 초기화.
    public void ClearSlot()
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
            Excute();
        }
    }
    public void Excute()
    {
        if (!Shop.isShopping) // 상점 UI 꺼져있을경우.
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    isEquip = true;
                    //장착.
                    if (item.weaponType == Item.WeaponType.Weapon)
                    {
                        theWeaponEquip.WeaponEquip(item, this);
                        SoundManager.inst.SFXPlay("SwordEquip");
                    }
                    else if (item.weaponType == Item.WeaponType.Shield)
                    {
                        theShieldEquip.ShieldEquip(item, this);
                        SoundManager.inst.SFXPlay("ShieldEquip");
                    }
                }
                else if(item.itemType == Item.ItemType.used)
                {
                    //소모.
                    Debug.Log(item.itemName + "을 사용했습니다.");
                    SoundManager.inst.SFXPlay("Drink");
                    SetSlotCount(-1);
                }
            }
        }
        else // 상점 ui 켜져있을경우
        {
            if (item != null && !isEquip)
            {
                StoreManager.Inst.SellPopupOpen(this);
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
    // 마우스가 슬롯에 들어갈 때 발동
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            theInven.ShowToolTip(item, transform.position);
        }
    }

    // 마우스가 슬롯에서 나갈 때 발동.
    public void OnPointerExit(PointerEventData eventData)
    {
        theInven.HideToolTip();
    }
}
