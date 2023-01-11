using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Equipment : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    public Item equipItem;
    public Slot equipSlot;
    public Image equipItemImage;
    
    
    public void WeaponEquip(Item item, Slot slot)
    {
        if (equipItem == null)
        {
            SetColor(1);
            equipItem = item;
            equipSlot = slot;
            equipItemImage.sprite = item.itemImage;
        }
        else
        {
            if(equipItem.itemName == item.itemName)
            {
                ClearSlot();
                equipSlot = slot;
            }
            else
            {
                equipSlot.isEquip = !equipSlot.isEquip;
                equipItem = item;
                equipSlot = slot;
                equipItemImage.sprite = item.itemImage;
            }
        }
    }
    public void ShieldEquip(Item item, Slot slot)
    {
        if (equipItem == null)
        {
            SetColor(1);
            equipItem = item;
            equipSlot = slot;
            equipItemImage.sprite = item.itemImage;
        }
        else
        {
            if (equipItem.itemName == item.itemName)
            {
                ClearSlot();
                equipSlot = slot;
            }
            else
            {
                equipSlot.isEquip = !equipSlot.isEquip;
                equipItem = item;
                equipSlot = slot;
                equipItemImage.sprite = item.itemImage;
            }
        }
    }
    private void SetColor(float _alpha)
    {
        Color color = equipItemImage.color;
        color.a = _alpha;
        equipItemImage.color = color;
    }
    private void ClearSlot()
    {
        equipSlot.isEquip = false;
        equipItem = null;
        equipItemImage.sprite = null;
        SetColor(0);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (equipItem != null)
            {
                if (equipItem.itemType == Item.ItemType.Equipment)
                {
                    //¿Â¬¯«ÿ¡¶
                    ClearSlot();
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item item = eventData.pointerDrag.transform.GetComponent<Slot>().item;
        Slot slot = eventData.pointerDrag.transform.GetComponent<Slot>();
        if(item.itemType == Item.ItemType.Equipment)
        {
            if(item.weaponType == Item.WeaponType.Weapon && transform.name == "WeaponSlot")
            {
                slot.isEquip = !slot.isEquip;
                WeaponEquip(item, slot);
            }
            else if(item.weaponType == Item.WeaponType.Shield && transform.name == "ShieldSlot")
            {
                slot.isEquip = !slot.isEquip;
                ShieldEquip(item, slot);
            }
        }
    }

    
}
