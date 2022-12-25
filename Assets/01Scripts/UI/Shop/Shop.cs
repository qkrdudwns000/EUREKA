using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static bool isShopping = false;
    public GameObject theShop;
    public GameObject inventoryBase;

    public Animator anim;

    private PlayerController thePlayer;
    public bool isShop = false;

    public void ZoneEnter()
    {
        anim.SetTrigger("Hello");
    }
    public void Enter(PlayerController player)
    {
        thePlayer = player;
        OpenShopUI();
    }
    public void Exit()
    {
        anim.SetTrigger("Hello");
        CloseShopUI();
    }
    public void Interaction()
    {
        Enter(thePlayer);
    }
    private void OpenShopUI()
    {
        theShop.SetActive(true);
        inventoryBase.SetActive(true);
        isShopping = true;
    }
    private void CloseShopUI()
    {
        theShop.SetActive(false);
        inventoryBase.SetActive(false);
        isShopping = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            ZoneEnter();
            isShop = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isShop = false;
        if (other.transform.tag == "Player")
        {
            Exit();
        }
    }
}
