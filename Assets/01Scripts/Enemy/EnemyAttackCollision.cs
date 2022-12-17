using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollision : MonoBehaviour
{
    int count;
    EnemyController theEnemy;
    private void Start()
    {
        theEnemy = GetComponentInParent<EnemyController>();
    }
    private void OnEnable()
    {
        count = 0;
        StartCoroutine("AutoDisable");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && count == 0)
        {
            other.GetComponent<PlayerController>().TakeDamage(theEnemy.myStat.AP);
            count++;
        }
    }
    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f); // Ȱ��ȭ������ 0.1�ʵڿ� ������Բ�.

        gameObject.SetActive(false);
    }
}
