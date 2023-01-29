using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
    public void Buy(int Num)
    {
        if ((GameManager.Inst.Gold - (item.itemPrice * Num)) < 0)
        {
            SoundManager.inst.SFXPlay("MainCancelPopup");
            StoreManager.Inst.go_dontEnough.SetActive(true);
            StoreManager.Inst.txt_DonEnough.text = "구매할 골드가\n부족합니다.";
            return;
        }
        else
        {
            theInven.AcquireItem(item, Num);
            if(Inventory.enoughSlot)
            {
                SoundManager.inst.SFXPlay("Coins");
                GameManager.Inst.Gold -= (item.itemPrice * Num);
            }
        }
        StoreManager.Inst.go_buyPopup.SetActive(false);
        StoreManager.Inst.go_usedBuyPopup.SetActive(false);
        if(!Inventory.enoughSlot)
        {
            SoundManager.inst.SFXPlay("MainCancelPopup");
            StoreManager.Inst.go_dontEnough.SetActive(true);
            StoreManager.Inst.txt_DonEnough.text = "배낭 공간이 충분하지 않습니다.";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            theInven.ShowToolTip(item, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theInven.HideToolTip();
    }
}
