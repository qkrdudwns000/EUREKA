using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlot : MonoBehaviour, IPointerClickHandler, IDropHandler
{

    public Item quickItem;
    public Skill quickSkill;
    public Image quickItemImage;
    private Slot orgSlot;
    private QuickSlotController quickSlotController;
    private PlayerController thePlayer;
    private PlayerEquipment theEquipment;
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
        thePlayer = FindObjectOfType<PlayerController>();
        theEquipment = FindObjectOfType<PlayerEquipment>();
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
            if(quickItem != null)
                img_CoolTime.fillAmount = currentCoolTime / quickItem.itemCoolTime;
            else if(quickSkill != null)
                img_CoolTime.fillAmount = currentCoolTime / quickSkill.skillCoolTime;


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
    public void SettingUsed(Item _item, int _itemCount = 1)
    {
        SetColor(1);
        quickItem = _item;
        quickItemImage.sprite = _item.itemImage;
        go_CountImage.SetActive(true);
        quickItemCount = _itemCount;
        text_Count.text = quickItemCount.ToString();
    }
    public void SettingSkill(Skill _skill)
    {
        SetColor(1);
        quickSkill = _skill;
        quickItemImage.sprite = _skill.skillImage;
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
    private void ClearSkillSlot()
    {
        quickSkill = null;
        quickItemImage.sprite = null;
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
    public void SkillUsed()
    {
        if(!isCoolTime && !SkillSetManager.isSkill && theEquipment.isEquipWeapon)
        {
            SkillSetManager.isSkill = true;
            currentCoolTime = quickSkill.skillCoolTime;
            isCoolTime = true;
            thePlayer.SkillPlay(quickSkill);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Slot slot = eventData.pointerDrag.transform.GetComponent<Slot>();
        Item item = null;
        if(slot != null)
        {
            item = eventData.pointerDrag.transform.GetComponent<Slot>().item;
        }
        
        SkillSlot skillSlot = eventData.pointerDrag.transform.GetComponent<SkillSlot>();
        Skill skill = null;

        if (skillSlot != null)
        {
            skill = eventData.pointerDrag.transform.GetComponent<SkillSlot>().skill;
        }

        if (slot != null)
        {
            if (quickItem == null && quickSkill == null)
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
                    if (quickItem != null)
                    {
                        quickSlotController.AcquireItem(quickItem, quickItemCount);
                    }
                    SettingUsed(item, slot);
                }
            }
        }
        else if(skillSlot != null)
        {
            if(quickSkill == null && quickItem == null)
            {
                SettingSkill(skill);
            }
            else
            {
                if (quickItem != null)
                {
                    if (quickItem.itemType == Item.ItemType.used)
                    {

                        quickSlotController.AcquireItem(quickItem, quickItemCount);
                        ClearSlot();

                        SettingSkill(skill);
                    }
                }
                else if(quickSkill != null)
                {
                    ClearSlot();

                    SettingSkill(skill);
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isCoolTime)
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
            else if(quickSkill != null)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    SkillUsed();
                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    ClearSkillSlot();
                }
            }
        }
    }
}
