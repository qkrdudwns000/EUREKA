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
}
