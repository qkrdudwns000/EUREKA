using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class Equipment : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    public Item equipItem;
    public Image equipItemImage;
    //public bool isEquip = false;
    public void WeaponEquip(Item item)
    {
        if (equipItem == null)
        {
            SetColor(1);
            equipItem = item;
            equipItemImage.sprite = item.itemImage;
        }
        else
        {
            if(equipItem.itemName == item.itemName)
            {
                ClearSlot();
            }
            else
            {
                equipItem = item;
                equipItemImage.sprite = item.itemImage;
            }
        }
    }
    public void ShieldEquip(Item item)
    {
        if (equipItem == null)
        {
            SetColor(1);
            equipItem = item;
            equipItemImage.sprite = item.itemImage;
        }
        else
        {
            if (equipItem.itemName == item.itemName)
            {
                ClearSlot();
            }
            else
            {
                equipItem = item;
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
        if(item.itemType == Item.ItemType.Equipment)
        {
            if(item.weaponType == Item.WeaponType.Weapon && transform.name == "WeaponSlot")
            {
                WeaponEquip(item);
            }
            else if(item.weaponType == Item.WeaponType.Shield && transform.name == "ShieldSlot")
            {
                ShieldEquip(item);
            }
        }
    }

    
}
