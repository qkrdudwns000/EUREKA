using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill4AttackColision : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private PlayerController theplayer;

    private float delayTime = 3.0f;
    private float curDelayTime = 3.0f;
    private bool isDelay = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.Translate(playerPos.transform.forward * Time.deltaTime * 10.0f);
        
    }
    private void OnEnable()
    {
        this.transform.position = playerPos.position + Vector3.up;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Enemy" && !isDelay)
        {
            isDelay = true;
            other.transform.GetComponent<EnemyController>().TakeDamage(50.0f);
            while(curDelayTime <= 0.0f)
            {
                curDelayTime -= Time.deltaTime;
            }
            curDelayTime = delayTime;
            isDelay = false;
        }
    }
}
