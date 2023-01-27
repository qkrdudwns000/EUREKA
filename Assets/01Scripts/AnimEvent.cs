using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent<bool> ComboCheck = default;
    [SerializeField] GameObject playerAttackCollision;
    [SerializeField] GameObject CounterAttackCollision;
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
    public void ConterAttackCollision()
    {
        CounterAttackCollision.SetActive(true);
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
    /////////////////////////////////////////// 보스 1 사운드
    ///

    public void BossFootStep(int i)
    {
        switch(i)
        {
            case 1:
                BossSM.inst.SFXPlay("BossFootStep1");
                break;
            case 2:
                int rnd = Random.Range(0, 2);
                if (rnd == 0)
                    BossSM.inst.SFXPlay("BossFootStep2");
                else
                    break;
                break;
        }
    }
    public void BossAttack1(int i)
    {
        switch(i)
        {
            case 1:
                BossSM.inst.SFXPlay("Boss_1_Attack_1");
                BossSM.inst.SFXPlay("Attack1");
                break;
            case 2:
                BossSM.inst.SFXPlay("Attack1");
                BossSM.inst.SFXPlay("Boss_2_Attack_1");
                break;
        }
    }
    public void BossAttack2(int i)
    {
        switch (i)
        {
            case 1:
                BossSM.inst.SFXPlay("Boss_1_Attack_2");
                BossSM.inst.SFXPlay("Attack2");
                break;
            case 2:
                BossSM.inst.SFXPlay("Boss_2_Attack_2");
                BossSM.inst.SFXPlay("Attack2");
                break;
        }
    }
    public void BossFinishAttack(int i)
    {
        switch(i)
        {
            case 1:
                BossSM.inst.SFXPlay("Boss_1_Finish");
                break;
            case 2:
                BossSM.inst.SFXPlay("Boss_2_Finish");
                break;
        }
    }
    public void BossEffectSound(int i)
    {
        switch(i)
        {
            case 1:
                BossSM.inst.SFXPlay("Explosion");
                break;
            case 2:
                break;
        }
    }

}
