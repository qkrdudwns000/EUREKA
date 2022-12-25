using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst = null;

    [SerializeField] int _gold = 0;

    public TMPro.TMP_Text text_gold;

    public int Gold
    {
        get => _gold;
        set => _gold = value;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        Gold = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        GoldManager();
    }

    private void GoldManager()
    {
        if(Shop.isShopping || Inventory.inventoryActivated)
        {
            text_gold.text = Gold.ToString();
        }
    }
}
