using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    PlayerController thePlayer;
    int count;
    private void Start()
    {
        thePlayer = GetComponentInParent<PlayerController>();
    }
    
    private void OnEnable()
    {
        count = 0;
        StartCoroutine("AutoDisable");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && count == 0)
        {
            Debug.Log("hi");
            other.GetComponent<EnemyController>().TakeDamage(thePlayer.myStat.AP);
            count++;
        }
    }
    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f); // Ȱ��ȭ������ 0.1�ʵڿ� ������Բ�.

        gameObject.SetActive(false);
    }
}
