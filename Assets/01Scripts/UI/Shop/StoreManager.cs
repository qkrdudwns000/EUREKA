using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager Inst;

    public GameObject go_dontEnough;
    public TMPro.TMP_Text txt_DonEnough;
    public TMPro.TMP_Text txt_ObjectNumber;
    public GameObject go_buyPopup;
    public GameObject go_usedBuyPopup;
    public GameObject go_sellPopup;

    private bool isBuyPopup = false;
    private bool isSellPopup = false;

    private int buyCount = 1;

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
        buyCount = 1;
        if (shopSlot.item.itemType == Item.ItemType.Equipment)
            go_buyPopup.SetActive(true);
        else
        {
            go_usedBuyPopup.SetActive(true);
            txt_ObjectNumber.text = buyCount.ToString();
        }
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
        shopSlot.Buy(buyCount);
        isBuyPopup = false;
        shopSlot = null;
    }
    public void BuyNo()
    {
        SoundManager.inst.SFXPlay("MainCancel");
        go_buyPopup.SetActive(false);
        go_usedBuyPopup.SetActive(false);
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
    public void AddNumber()
    {
        SoundManager.inst.SFXPlay("MainConfirm");
        if(shopSlot.item.itemPrice * (buyCount + 1) <= GameManager.Inst.Gold)
           buyCount++;

        txt_ObjectNumber.text = buyCount.ToString();
    }
    public void ReduceNumver()
    {
        SoundManager.inst.SFXPlay("MainCancel");
        if(buyCount > 1)
           buyCount--;
        txt_ObjectNumber.text = buyCount.ToString();
    }
}
