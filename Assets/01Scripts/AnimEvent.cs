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
    // 어택 collision 활성화 이벤트
    public void OnAttackCollision()
    {
        playerAttackCollision.SetActive(true);
    }

    public void Skill_2_CollisionStart()
    {
        skill2AttackCollision.SetActive(true);
    }
    public void Skill_3_CollisionStart()
    {
        thePlayer.Skill_3_AttackCollision();
    }
    public void Skill_4_CollisionStart()
    {
        skill4AttackCollision.SetActive(true);
    }
    public void Skill_4_CollisionEnd()
    {
        skill4AttackCollision.SetActive(false);
    }
}
