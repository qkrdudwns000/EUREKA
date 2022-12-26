using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private TMPro.TMP_Text txt_ItemName;
    [SerializeField]
    private TMPro.TMP_Text txt_ItemDesc;
    [SerializeField]
    private TMPro.TMP_Text txt_ItemHowtoUsed;
    [SerializeField]
    private TMPro.TMP_Text txt_ItemType;
    [SerializeField]
    private TMPro.TMP_Text txt_Price;




    public void ShowToolTip(Item _item)
    {
        go_Base.SetActive(true);

        txt_ItemName.text = _item.itemName;
        txt_ItemName.text = _item.itemDesc;
        txt_Price.text = _item.itemPrice.ToString();

        if (_item.itemType == Item.ItemType.Equipment)
        {
            txt_ItemHowtoUsed.text = "우클릭 - 장착";
            txt_ItemType.text = "장비";
        }
        else if (_item.itemType == Item.ItemType.used)
        {
            txt_ItemHowtoUsed.text = "우클릭 - 사용";
            txt_ItemType.text = "소비";
        }
        else
        {
            txt_ItemHowtoUsed.text = "";
            txt_ItemType.text = "";
        }
    }
    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }
}
