using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static bool isShopping = false;
    public GameObject theShop;
    public GameObject inventoryBase;
    [SerializeField] private GameObject go_EnabelInteration;

    public Animator anim;

    public bool isShop = false;


    public void ZoneEnter()
    {
        anim.SetTrigger("Hello");
        go_EnabelInteration.SetActive(true);
    }
    public void Enter()
    {
        OpenShopUI();
    }
    public void Exit()
    {
        anim.SetTrigger("Hello");
        go_EnabelInteration.SetActive(false);
        CloseShopUI();
    }
    public void Interaction()
    {
        go_EnabelInteration.SetActive(false);
        GameManager.Inst.Action(this.gameObject);
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
