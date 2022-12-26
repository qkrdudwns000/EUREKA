using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager Inst;

    public GameObject go_dontEnough;
    public GameObject go_buyPopup;
    public GameObject go_sellPopup;

    ShopSlot shopSlot = null;
    Slot slot = null;

    private void Awake()
    {
        Inst = this;
    }

    public void BuyPopupOpen(ShopSlot _shopslot)
    {
        shopSlot = _shopslot;
        go_buyPopup.SetActive(true); 
    }
    public void SellPopupOpen(Slot _slot)
    {
        slot = _slot;
        go_sellPopup.SetActive(true);
    }
    public void BuyYes()
    {
        shopSlot.Buy();
        shopSlot = null;
    }
    public void BuyNo()
    {
        shopSlot = null;
    }
    public void SellYes()
    {
        slot.SellItem();
        slot = null;
        go_sellPopup.SetActive(false);
    }
    public void SellNo()
    {
        slot = null;
        go_sellPopup.SetActive(false);
    }
    public void CloseDontEnoughPopup()
    {
        go_dontEnough.SetActive(false);
    }
}
