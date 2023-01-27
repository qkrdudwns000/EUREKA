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
    [SerializeField]
    private TMPro.TMP_Text txt_ValueName;
    [SerializeField]
    private TMPro.TMP_Text txt_Value;



    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        go_Base.SetActive(true);
        _pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 0.5f, -go_Base.GetComponent<RectTransform>().rect.height * 0.5f, 0.0f);
        go_Base.transform.position = _pos;

        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;
        txt_Value.text = _item.itemValue.ToString();
        txt_Price.text = (_item.itemPrice * 0.5f).ToString();

        if (_item.itemType == Item.ItemType.Equipment)
        {
            txt_ItemHowtoUsed.text = "��Ŭ�� - ����";
            txt_ItemType.text = "���";
            if (_item.weaponType == Item.WeaponType.Weapon)
                txt_ValueName.text = "<color=#B7342A>" + "���ݷ�";
            else if (_item.weaponType == Item.WeaponType.Shield)
                txt_ValueName.text = "<color=#2A37B7>" + "����";
        }
        else if (_item.itemType == Item.ItemType.used)
        {
            txt_ItemHowtoUsed.text = "��Ŭ�� - ���";
            txt_ItemType.text = "�Һ�";
            txt_ValueName.text = "<color=#0C7E32>" + "ȸ����";
        }
        else
        {
            txt_ItemHowtoUsed.text = "�Ǹ�����";
            txt_ItemType.text = "��Ÿ";
            txt_ValueName.text = "";
            txt_Value.text = "";
        }
    }
    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }
}
