using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent<bool> ComboCheck = default;
    [SerializeField] GameObject playerAttackCollision;
    private PlayerController thePlayer;

    [SerializeField] GameObject skill4AttackCollision;
    [SerializeField] GameObject skill2AttackCollision;
    private void Start()
    {
        thePlayer = GetComponentInParent<PlayerController>();
    }

    // 콤보어택 시작 이벤트
    public void ComboCheckStart()
    {
        ComboCheck?.Invoke(true);
    }
    // 콤보어택 끝 이벤트
    public void ComboCheckEnd()
    {
        ComboCheck?.Invoke(false);
    }
    public void StaminaDecrease()
    {
        thePlayer.StaminaControl();
    }
    public void SkillEnd()
    {
        SkillSetManager.isSkill = false;
    }

    // 어택 collision 활성화 이벤트
    public void OnAttackCollision()
    {
        playerAttackCollision.SetActive(true);
    }

    public void Skill_2_CollisionStart()
    {
        skill2AttackCollision.SetActive(true);
        SoundManager.inst.SFXPlay("Flash");
    }
    public void Skill_2_Sound()
    {
        SoundManager.inst.SFXPlay("Flash1");
    }
    public void Skill_3_CollisionStart()
    {
        thePlayer.Skill_3_AttackCollision();
        SoundManager.inst.SFXPlay("Explosion");
    }
    public void Skill_4_CollisionStart()
    {
        skill4AttackCollision.SetActive(true);
    }
    public void Skill_4_Sound()
    {
        SoundManager.inst.SFXPlay("Fire2");
    }
    public void Skill_4_CollisionEnd()
    {
        skill4AttackCollision.SetActive(false);
    }
    public void FootStepSound()
    {
        SoundManager.inst.SFXPlay("FootStep");
    }

    public void AttackSound(int i)
    {
        switch(i)
        {
            case 1:
                SoundManager.inst.SFXPlay("AttackHigh1");
                break;
            case 2:
                SoundManager.inst.SFXPlay("AttackHigh2");
                break;
            case 3:
                SoundManager.inst.SFXPlay("AttackHigh3");
                SoundManager.inst.SFXPlay("Attack1");
                SoundManager.inst.SFXPlay("Fire1");
                break;
            case 4:
                SoundManager.inst.SFXPlay("AttackMid1");
                break;
            case 5:
                SoundManager.inst.SFXPlay("AttackMid2");
                break;
            case 6:
                SoundManager.inst.SFXPlay("AttackMid3");
                SoundManager.inst.SFXPlay("Attack1");
                break;
        }
    }


}
