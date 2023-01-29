using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorMenu : MonoBehaviour
{
    [MenuItem("AutoGrowth/ExperienceUp/60Exp Up")]
    static void Exp60Up()
    {
        GameManager.Inst.Experience60Btn();
    }

    [MenuItem("AutoGrowth/ExperienceUp/300Exp Up")]
    static void Exp300Up()
    {
        GameManager.Inst.Experience300Btn();
    }

    [MenuItem("AutoGrowth/GoldUp/500Gold Up")]
    static void Gold500Up()
    {
        GameManager.Inst.Gold += 500;
    }

    [MenuItem("AutoGrowth/GoldUp/1000Gold Up")]
    static void Gold1000Up()
    {
        GameManager.Inst.Gold += 1000;
    }
}
