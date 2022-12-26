using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;
    public static bool enoughSlot = true;
    

    // ÇÊ¿äÇÑ ÄÄÆÛ³ÍÆ®
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_EquipBase;
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private Equipment theWeaponEquip;
    [SerializeField]
    private Equipment theShieldEquip;
    [SerializeField]
    private PlayerEquipment thePlayerEquipment;
    [SerializeField]
    private SlotToolTip theSlotToolTip;


    // ½½·Ôµé
    private Slot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Shop.isShopping)
        {
            TryOpenInventory();
            if (inventoryActivated)
            {
                CheckEquip();
                thePlayerEquipment.WeaponSwap();
                thePlayerEquipment.ShieldSwap();
            }
        }
        
    }
    public void ShowToolTip(Item _item)
    {
        theSlotToolTip.ShowToolTip(_item);
    }
    public void HideToolTip()
    {
        theSlotToolTip.HideToolTip();
    }

    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;
            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
        go_EquipBase.SetActive(true);
    }
    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
        go_EquipBase.SetActive(false);
    }
    public void AcquireItem(Item _item, int _count)
    {
        if(Item.ItemType.Equipment != _item.itemType )
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
        
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                enoughSlot = true;
                slots[i].AddItem(_item, _count);
                return;
            }
        }
        enoughSlot = false; // ÀÎº¥ ²ËÃ¡À»¶§
    }
    public void CheckEquip()
    {
        for(int i = 0; i< slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].isEquip)
                {
                    slots[i].go_EquipImage.SetActive(true);
                }
                else
                {
                    slots[i].go_EquipImage.SetActive(false);
                }
            }
        }
    }
}
