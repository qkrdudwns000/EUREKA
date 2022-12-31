using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager Inst;

    public GameObject go_dontEnough;
    public GameObject go_buyPopup;
    public GameObject go_sellPopup;

    private bool isBuyPopup = false;
    private bool isSellPopup = false;

    ShopSlot shopSlot = null;
    Slot slot = null;

    private void Awake()
    {
        Inst = this;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (isBuyPopup)
                BuyYes();
            else if (isSellPopup)
                SellYes();
        }
    }

    public void BuyPopupOpen(ShopSlot _shopslot)
    {
        shopSlot = _shopslot;
        go_buyPopup.SetActive(true);
        isBuyPopup = true;
    }
    public void SellPopupOpen(Slot _slot)
    {
        slot = _slot;
        isSellPopup = true;
        go_sellPopup.SetActive(true);
    }
    public void BuyYes()
    {
        shopSlot.Buy();
        isBuyPopup = false;
        shopSlot = null;
    }
    public void BuyNo()
    {
        isBuyPopup = false;
        shopSlot = null;
    }
    public void SellYes()
    {
        slot.SellItem();
        isSellPopup = false;
        slot = null;
        go_sellPopup.SetActive(false);
    }
    public void SellNo()
    {
        slot = null;
        isSellPopup = false;
        go_sellPopup.SetActive(false);
    }
    public void CloseDontEnoughPopup()
    {
        go_dontEnough.SetActive(false);
    }
}
