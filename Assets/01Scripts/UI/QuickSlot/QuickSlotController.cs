using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotController : MonoBehaviour
{
    [SerializeField] private QuickSlot[] quickSlots;
    [SerializeField] private Transform tf_parent;

    private int selectedSlot; // ���õ� ������. (0~7)
    

    [SerializeField]
    private GameObject go_SelectedImage;
    [SerializeField]
    private Inventory theInven;

    public QuickSlot[] GetQuickSlots() { return quickSlots; }
    [SerializeField] private Skill[] skills;
    [SerializeField] private Item[] usedItems;
    public void LoadToQuickItem(int _arrayNum, string _itemName, int _itemNum = 1)
    {
        for (int i = 0; i < usedItems.Length; i++)
        {
            if (usedItems[i].itemName == _itemName)
            {
                quickSlots[_arrayNum].SettingUsed(usedItems[i], _itemNum);
            }
        }
    }
    public void LoadToQuickSkill(int _arrayNum, int _skillID)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].skillID == _skillID)
            {
                quickSlots[_arrayNum].SettingSkill(skills[i]);
            }
        }
    }
    private void Awake()
    {
        quickSlots = tf_parent.GetComponentsInChildren<QuickSlot>();
    }

    void Start()
    {
        selectedSlot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TryInputNumber();
    }

    private void TryInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeSlot(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            ChangeSlot(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            ChangeSlot(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            ChangeSlot(7);
    }
    private void ChangeSlot(int _num)
    {
        SelectedSlot(_num);

        Execute();
    }
    private void SelectedSlot(int _num)
    {
        selectedSlot = _num;
        //���õ� �������� �̹����̵�.
        go_SelectedImage.transform.position = quickSlots[selectedSlot].transform.position;
    }

    private void Execute()
    {
        if (quickSlots[selectedSlot].quickItem != null)
        {
            if (quickSlots[selectedSlot].quickItem.itemType == Item.ItemType.used)
                quickSlots[selectedSlot].PotionUsed(-1);
        }
        else if (quickSlots[selectedSlot].quickSkill != null)
        {
            quickSlots[selectedSlot].SkillUsed();
        }
    }
    public void AcquireItem(Item _item, int _count)
    {
        theInven.AcquireItem(_item, _count);
    }
}
