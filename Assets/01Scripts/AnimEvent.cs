using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent<bool> ComboCheck = default;
    [SerializeField] GameObject playerAttackCollision;
    private PlayerController thePlayer;
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
}
