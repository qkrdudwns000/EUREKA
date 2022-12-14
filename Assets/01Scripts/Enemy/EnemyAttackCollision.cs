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
        yield return new WaitForSeconds(0.1f); // 활성화된직후 0.1초뒤에 사라지게끔.

        gameObject.SetActive(false);
    }
}
