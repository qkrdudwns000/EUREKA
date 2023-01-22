using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager Inst;

    public GameObject go_dontEnough;
    public TMPro.TMP_Text txt_DonEnough;
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
        SoundManager.inst.SFXPlay("MainConfirm");
        shopSlot = _shopslot;
        go_buyPopup.SetActive(true);
        isBuyPopup = true;
    }
    public void SellPopupOpen(Slot _slot)
    {
        SoundManager.inst.SFXPlay("MainConfirm");
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
        SoundManager.inst.SFXPlay("MainCancel");
        go_buyPopup.SetActive(false);
        isBuyPopup = false;
        shopSlot = null;
    }
    public void SellYes()
    {
        SoundManager.inst.SFXPlay("Coins");
        slot.SellItem();
        isSellPopup = false;
        slot = null;
        go_sellPopup.SetActive(false);
    }
    public void SellNo()
    {
        SoundManager.inst.SFXPlay("MainCancel");
        slot = null;
        isSellPopup = false;
        go_sellPopup.SetActive(false);
    }
    public void CloseDontEnoughPopup()
    {
        SoundManager.inst.SFXPlay("MainCancel");
        go_dontEnough.SetActive(false);
    }
}
