using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent<bool> ComboCheck = default;
    [SerializeField] GameObject attackCollision;
    
    
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
    // �������� �� �̺�Ʈ
    public void RollingEnd()
    {
        PlayerController thePlayer = GetComponentInParent<PlayerController>();
        thePlayer.RollingEnd();
    }
    // ���� collision Ȱ��ȭ �̺�Ʈ
    public void OnAttackCollision()
    {
        attackCollision.SetActive(true);
    }
}
