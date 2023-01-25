using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Inventory theInven;
    public QuestManager theQuest;
    public SkillSetManager theSkill;
    public PauseMenu thePauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
            if (Inventory.inventoryActivated || QuestManager.isQuestPopup || SkillSetManager.isSkillSetting || GameManager.isPause)
            {
                theInven.CloseInventory();
                theQuest.CloseQuestPopup();
                theSkill.CloseSkillSet();
                thePauseMenu.CloseMenu();
            }
            else
                thePauseMenu.TryOpenMenu();
        }
            
    }
}
