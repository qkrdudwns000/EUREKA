using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public GameObject[] Weapons; // ���������.
    private GameObject EquipWeaponObject = null; // �������� ���� ���庯��

    public Equipment EquipWeapon; // �������� ����
    public Equipment EquipShield; // �������� ����
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
                case "������":
                    ClearEquip(0);
                    Weapons[0].SetActive(true);
                    break;
                case "ö��":
                    ClearEquip(1);
                    Weapons[1].SetActive(true);
                    break;
                case "���":
                    ClearEquip(2);
                    Weapons[2].SetActive(true);
                    break;
                case "�ٶ���":
                    ClearEquip(3);
                    Weapons[3].SetActive(true);
                    break;
                case "���ǰ�":
                    ClearEquip(4);
                    Weapons[4].SetActive(true);
                    break;
                case "�Ͱ�":
                    ClearEquip(5);
                    Weapons[5].SetActive(true);
                    break;
                case "���հ�":
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
