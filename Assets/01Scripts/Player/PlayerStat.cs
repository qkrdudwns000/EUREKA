using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public struct PlayerStat
{
    [SerializeField] float hp;
    [SerializeField] float maxHp;
    [SerializeField] float sp;
    [SerializeField] float maxSp;
    [SerializeField] float ap;

    public UnityAction<float> changeHp;
    public UnityAction<float> changeSp;
    public float HP
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0.0f, maxHp);
            changeHp?.Invoke(hp / maxHp);
        }
    }
    public float SP
    {
        get => sp;
        set
        {
            sp = Mathf.Clamp(value, 0.0f, maxSp);
            changeSp?.Invoke(sp / maxSp);
        }
    }
    public float AP
    {
        get => ap;
    }

    public float maxHP
    {
        get => maxHp;
    }
    public float maxSP
    {
        get => maxSp;
    }
}
