using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; //아이템 이름.
    [TextArea]
    public string itemDesc; //아이템 설명.
    public ItemType itemType;
    public WeaponType weaponType;
    public Sprite itemImage; // 아이템의 이미지.
    public int itemPrice;
    public float itemCoolTime;
    public GameObject itemPrefab; // 아이템의 프리팹.

    
    public enum WeaponType
    {
        Weapon,
        Shield
    }
    
    public enum ItemType
    {
        Equipment,
        used,
        Ingredient
    }
}
