using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    int count;
    private void OnEnable()
    {
        count = 0;
        StartCoroutine("AutoDisable");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && count == 0)
        {
            other.GetComponent<EnemyCotroller>().TakeDamage(10);
            count++;
        }
    }
    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f); // 활성화된직후 0.1초뒤에 사라지게끔.

        gameObject.SetActive(false);
    }
}
