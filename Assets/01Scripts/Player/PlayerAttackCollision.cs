using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyCotroller>().TakeDamage(10);
        }
    }
    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f); // Ȱ��ȭ������ 0.1�ʵڿ� ������Բ�.

        gameObject.SetActive(false);
    }
}
