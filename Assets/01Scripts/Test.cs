using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();
        gameManager.SetLevelSystem(levelSystem);
    }
}
