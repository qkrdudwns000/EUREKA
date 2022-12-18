using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; //아이템 이름.
    public ItemType itemType;
    public Sprite itemImage; // 아이템의 이미지.
    public GameObject itemPrefab; // 아이템의 프리팹.

    public string weaponType; // 무기 종류
    
    public enum ItemType
    {
        Equipment,
        used,
        Ingredient
    }
}
