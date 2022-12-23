using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public GameObject[] Weapons; // ���������.
    private GameObject EquipWeaponObject = null; // �������� ���� ���庯��

    public GameObject[] Shields; // ���� �����
    private GameObject EquipShieldObject = null; // �������� ���� ���庯��.

    public bool isEquipWeapon = false;
    public bool isEquipShield = false;

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
            isEquipWeapon = true;
            switch(EquipWeapon.equipItem.itemName)
            {
                case "������":
                    ClearWeapon(0);
                    Weapons[0].SetActive(true);
                    break;
                case "ö��":
                    ClearWeapon(1);
                    Weapons[1].SetActive(true);
                    break;
                case "���":
                    ClearWeapon(2);
                    Weapons[2].SetActive(true);
                    break;
                case "�ٶ���":
                    ClearWeapon(3);
                    Weapons[3].SetActive(true);
                    break;
                case "���ǰ�":
                    ClearWeapon(4);
                    Weapons[4].SetActive(true);
                    break;
                case "�Ͱ�":
                    ClearWeapon(5);
                    Weapons[5].SetActive(true);
                    break;
                case "���հ�":
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
                case "��������":
                    ClearShield(0);
                    Shields[0].SetActive(true);
                    break;
                case "ö����":
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
