using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public GameObject[] Weapons; // 장착무기들.
    private GameObject EquipWeaponObject = null; // 장착중인 무기 저장변수

    public Equipment EquipWeapon; // 장착중인 무기
    public Equipment EquipShield; // 장착중인 쉴드
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void WeaponSwap()
    {
        if (EquipWeapon.equipItem != null)
        {
            switch(EquipWeapon.equipItem.itemName)
            {
                case "낡은검":
                    ClearEquip(0);
                    Weapons[0].SetActive(true);
                    break;
                case "철검":
                    ClearEquip(1);
                    Weapons[1].SetActive(true);
                    break;
                case "용검":
                    ClearEquip(2);
                    Weapons[2].SetActive(true);
                    break;
                case "바람검":
                    ClearEquip(3);
                    Weapons[3].SetActive(true);
                    break;
                case "달의검":
                    ClearEquip(4);
                    Weapons[4].SetActive(true);
                    break;
                case "귀검":
                    ClearEquip(5);
                    Weapons[5].SetActive(true);
                    break;
                case "마왕검":
                    ClearEquip(6);
                    Weapons[6].SetActive(true);
                    break;
            }
        }
        else
        {
            if (EquipWeaponObject != null)
            {
                EquipWeaponObject.SetActive(false);
                EquipWeaponObject = null;
            }
        }
    }
    private void ClearEquip(int index)
    {
        if (EquipWeaponObject != null)
        {
            EquipWeaponObject.SetActive(false);
            EquipWeaponObject = Weapons[index];
        }
        else
        {
            EquipWeaponObject = Weapons[index];
        }
    }
}
