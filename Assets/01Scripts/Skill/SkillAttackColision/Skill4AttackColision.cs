using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill4AttackColision : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private PlayerController theplayer;


    // Update is called once per frame
    void Update()
    {
        
        this.transform.Translate(playerPos.transform.forward * Time.deltaTime * 10.0f);
        
    }
    private void OnEnable()
    {
        this.transform.position = playerPos.position + Vector3.up;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Enemy")
        {
            other.transform.GetComponent<EnemyController>().TakeDamage(theplayer.myStat.AP * 2.5f);
        }
    }
}
