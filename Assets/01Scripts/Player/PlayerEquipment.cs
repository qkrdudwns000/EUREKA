using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public GameObject[] Weapons; // 장착무기들.
    private GameObject EquipWeaponObject = null; // 장착중인 무기 저장변수

    public GameObject[] Shields; // 장착 쉴드들
    private GameObject EquipShieldObject = null; // 장착중인 쉴드 저장변수.

    public bool isEquipWeapon = false;
    public bool isEquipShield = false;

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
            isEquipWeapon = true;
            switch(EquipWeapon.equipItem.itemName)
            {
                case "낡은검":
                    ClearWeapon(0);
                    Weapons[0].SetActive(true);
                    break;
                case "철검":
                    ClearWeapon(1);
                    Weapons[1].SetActive(true);
                    break;
                case "용검":
                    ClearWeapon(2);
                    Weapons[2].SetActive(true);
                    break;
                case "바람검":
                    ClearWeapon(3);
                    Weapons[3].SetActive(true);
                    break;
                case "달의검":
                    ClearWeapon(4);
                    Weapons[4].SetActive(true);
                    break;
                case "귀검":
                    ClearWeapon(5);
                    Weapons[5].SetActive(true);
                    break;
                case "마왕검":
                    ClearWeapon(6);
                    Weapons[6].SetActive(true);
                    break;
            }
        }
        else
        {
            isEquipWeapon = false;
            if (EquipWeaponObject != null)
            {
                EquipWeaponObject.SetActive(false);
                EquipWeaponObject = null;
            }
        }
    }
    public void ShieldSwap()
    {
        if (EquipShield.equipItem != null)
        {
            isEquipShield = true;
            switch (EquipShield.equipItem.itemName)
            {
                case "나무방패":
                    ClearShield(0);
                    Shields[0].SetActive(true);
                    break;
                case "철방패":
                    ClearShield(1);
                    Shields[1].SetActive(true);
                    break;
            }
        }
        else
        {
            isEquipShield = false;
            if (EquipShieldObject != null)
            {
                EquipShieldObject.SetActive(false);
                EquipShieldObject = null;
            }
        }
    }
    private void ClearWeapon(int index)
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
    private void ClearShield(int index)
    {
        if (EquipShieldObject != null)
        {
            EquipShieldObject.SetActive(false);
            EquipShieldObject = Shields[index];
        }
        else
        {
            EquipShieldObject = Shields[index];
        }
    }
}
