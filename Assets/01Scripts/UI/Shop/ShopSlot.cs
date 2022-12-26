using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour, IPointerClickHandler
{
    public Inventory theInven;

    public Item item;
    public Image itemImage;

    public TMPro.TMP_Text itemPrice;
    public TMPro.TMP_Text itemName;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            StoreManager.Inst.BuyPopupOpen(this);
        }
    }
    public void Buy()
    {
        if ((GameManager.Inst.Gold - item.itemPrice) < 0)
        {
            return;
        }
        else
        {
            theInven.AcquireItem(item, 1);
            if(Inventory.enoughSlot)
            {
                GameManager.Inst.Gold -= item.itemPrice;
            }
        }
        StoreManager.Inst.go_buyPopup.SetActive(false);
        if(!Inventory.enoughSlot)
        {
            StoreManager.Inst.go_dontEnough.SetActive(true);
        }
    }
}
