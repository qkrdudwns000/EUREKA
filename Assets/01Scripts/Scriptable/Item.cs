using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; //������ �̸�.
    public ItemType itemType;
    public WeaponType weaponType;
    public Sprite itemImage; // �������� �̹���.
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
