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

    public GameObject go_SlotsParent;
    [SerializeField]
    private Equipment theWeaponEquip;
    [SerializeField]
    private Equipment theShieldEquip;
    [SerializeField]
    private PlayerEquipment thePlayerEquipment;
    [SerializeField]
    private SlotToolTip theSlotToolTip;


    // ½½·Ôµé
    public Slot[] slots;

    public Slot[] GetSlots() { return slots; }
    [SerializeField] private Item[] item;
    public void LoadToInven(int _arrayNum, string _itemName, int _itemNum, bool _isEquip)
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i].itemName == _itemName)
            {
                slots[_arrayNum].AddItem(item[i], _itemNum);
                slots[_arrayNum].isEquip = _isEquip;

                if (slots[_arrayNum].isEquip)
                    slots[_arrayNum].EquipCheck();
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }
    private void Start()
    {
        thePlayerEquipment.WeaponSwap();
        thePlayerEquipment.ShieldSwap();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Shop.isShopping)
        {
            if (Input.GetKeyDown(KeyCode.I))
                TryOpenInventory();

            if (inventoryActivated)
            {
                CheckEquip();
                
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
            CloseInventory();
        
        
    }
    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        theSlotToolTip.ShowToolTip(_item, _pos);
    }
    public void HideToolTip()
    {
        theSlotToolTip.HideToolTip();
    }

    public void TryOpenInventory()
    {
        if (!inventoryActivated)
            OpenInventory();
        else
            CloseInventory();
    }

    private void OpenInventory()
    {
        inventoryActivated = true;
        go_InventoryBase.SetActive(true);
        go_EquipBase.SetActive(true);
    }
    private void CloseInventory()
    {
        inventoryActivated = false;
        go_InventoryBase.SetActive(false);
        go_EquipBase.SetActive(false);
        HideToolTip();
    }
    public void AcquireItem(Item _item, int _count)
    {
        if(Item.ItemType.Equipment != _item.itemType)
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
        thePlayerEquipment.WeaponSwap();
        thePlayerEquipment.ShieldSwap();
    }
}
