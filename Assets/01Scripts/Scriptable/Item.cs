using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; //������ �̸�.
    [TextArea]
    public string itemDesc; //������ ����.
    public ItemType itemType;
    public WeaponType weaponType;
    public Sprite itemImage; // �������� �̹���.
    public int itemPrice;
    public float itemCoolTime;
    public GameObject itemPrefab; // �������� ������.

    
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
