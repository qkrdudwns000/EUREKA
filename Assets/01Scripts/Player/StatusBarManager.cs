using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarManager : MonoBehaviour
{
    [SerializeField] PlayerController thePlayer;

    [SerializeField] Image[] image_Gause;

    private const int HP = 0, SP = 1;

    private void Update()
    {
        GauseUpdate();
    }
    private void GauseUpdate()
    {
        image_Gause[HP].fillAmount = thePlayer.myStat.HP / thePlayer.myStat.maxHP;
        image_Gause[SP].fillAmount = thePlayer.myStat.SP / thePlayer.myStat.maxSP;
    }
}
