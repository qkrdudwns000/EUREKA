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

    // �޺����� ���� �̺�Ʈ
    public void ComboCheckStart()
    {
        ComboCheck?.Invoke(true);
    }
    // �޺����� �� �̺�Ʈ
    public void ComboCheckEnd()
    {
        ComboCheck?.Invoke(false);
    }
    public void StaminaDecrease()
    {
        thePlayer.StaminaControl();
    }
    // ���� collision Ȱ��ȭ �̺�Ʈ
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
