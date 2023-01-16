using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterProperty : MonoBehaviour
{
    public MonsterStat myStat;
    public Animator _anim = null;

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
