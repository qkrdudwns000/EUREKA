using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2AttackColision : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;
    private PlayerController thePlayer;
    // Start is called before the first frame update

    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }
    private void OnEnable()
    {
        this.transform.position = playerPos.position + Vector3.up + playerPos.forward * 2.0f;
        this.transform.LookAt(playerPos.forward);
        StartCoroutine("AutoDisable");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.transform.GetComponent<EnemyController>().TakeDamage(thePlayer.myStat.AP * 2);
        }
    }

    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
    }
}
