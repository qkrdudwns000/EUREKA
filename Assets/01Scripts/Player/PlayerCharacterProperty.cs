using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterProperty : MonoBehaviour
{
    public PlayerStat myStat;
    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if(_anim == null)
            {
                _anim = GetComponent<Animator>();
                if(_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>(); // �θ𿡾����� �ڽĿ���ã��
                }
            }
            return _anim;
        }
    }
}
