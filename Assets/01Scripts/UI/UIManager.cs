using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Inventory theInven;
    public QuestManager theQuest;
    public SkillSetManager theSkill;
    public Shop theShop;
    public PauseMenu thePauseMenu;


    // Update is called once per frame
    void Update()
    {
        if (!Shop.isShopping)
        {
            if (Input.GetKeyDown(KeyCode.I))
                theInven.TryOpenInventory();
            else if (Input.GetKeyDown(KeyCode.Q))
                theQuest.TryOpenQuestPopup();
            else if (Input.GetKeyDown(KeyCode.K))
                theSkill.TryOpenSkillSet();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Inventory.inventoryActivated || QuestManager.isQuestPopup || SkillSetManager.isSkillSetting || GameManager.isPause || Shop.isShopping)
            {
                theInven.CloseInventory();
                theQuest.CloseQuestPopup();
                theSkill.CloseSkillSet();
                if(theShop != null)
                   theShop.CloseShopUI();

                thePauseMenu.CloseMenu();
            }
            else
                thePauseMenu.TryOpenMenu();
        }
            
    }
}
