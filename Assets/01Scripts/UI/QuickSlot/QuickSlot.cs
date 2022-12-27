using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlot : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    public Item quickItem;
    public Image quickItemImage;
    private Slot orgSlot;
    private QuickSlotController quickSlotController;
    public int quickItemCount;

    private float currentCoolTime;
    private bool isCoolTime;

    [SerializeField]
    private TMPro.TMP_Text text_Count;
    [SerializeField]
    private GameObject go_CountImage; // 아이템 카운팅 이미지
    [SerializeField] 
    private Image img_CoolTime;

    private void Start()
    {
        quickSlotController = GetComponentInParent<QuickSlotController>();
    }
    private void Update()
    {
        CoolTimeCalc();
    }

    private void CoolTimeCalc()
    {
        if (isCoolTime)
        {
            currentCoolTime -= Time.deltaTime;
            img_CoolTime.fillAmount = currentCoolTime / quickItem.itemCoolTime;

            if (currentCoolTime <= 0)
                isCoolTime = false;
        }
    }

    private void SettingUsed(Item _item, Slot _slot)
    {
        SetColor(1);
        quickItem = _item;
        quickItemImage.sprite = _item.itemImage;
        go_CountImage.SetActive(true);
        orgSlot = _slot;
        quickItemCount = orgSlot.itemCount;
        text_Count.text = quickItemCount.ToString();
        orgSlot.ClearSlot();
    }

    private void SetColor(float _alpha)
    {
        Color color = quickItemImage.color;
        color.a = _alpha;
        quickItemImage.color = color;
    }
    private void ClearSlot()
    {
        quickItem = null;
        quickItemImage.sprite = null;
        quickItemCount = 0;
        text_Count.text = "0";
        go_CountImage.SetActive(false);

        SetColor(0);
    }
    public void PotionUsed(int _count)
    {
        if (!isCoolTime)
        {
            if (quickItemCount + _count > 0)
            {
                currentCoolTime = quickItem.itemCoolTime;
                isCoolTime = true;
            }

            quickItemCount += _count;
            text_Count.text = quickItemCount.ToString();
            Debug.Log("체력이 회복되었습니다.");
            if (quickItemCount <= 0)
            {
                ClearSlot();
            }
        }
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item item = eventData.pointerDrag.transform.GetComponent<Slot>().item;
        Slot slot = eventData.pointerDrag.transform.GetComponent<Slot>();
        if (quickItem == null)
        {
            if (item.itemType == Item.ItemType.used)
            {
                SettingUsed(item, slot);
            }
        }
        else
        {
            if (item.itemType == Item.ItemType.used)
            {
                quickSlotController.AcquireItem(quickItem, quickItemCount);
                SettingUsed(item, slot);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (quickItem != null)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                PotionUsed(-1);
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                quickSlotController.AcquireItem(quickItem, quickItemCount);
                ClearSlot();
            }
        }
    }
}
