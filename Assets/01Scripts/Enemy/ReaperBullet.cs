using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperBullet : MagicBullet
{
    private GameObject go_Player;
    private Rigidbody myRigid;
    private bool isReady = false;
    [SerializeField] private float bulletSpeed = 17.0f;
    [SerializeField] private float turnSpeed = 360.0f;
    
    

    private void Awake()
    {
        //nav = GetComponent<NavMeshAgent>();
        go_Player = GameObject.Find("Player");
        myRigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        isReady = false;
        StartCoroutine(StartMove());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isReady)
        {
            TargetingMove();
        }
    }

    private void TargetingMove()
    {
        myRigid.velocity = transform.forward * bulletSpeed;
        var bulletTargetRotation = Quaternion.LookRotation(go_Player.transform.position - transform.position);
        myRigid.MoveRotation(Quaternion.RotateTowards(transform.rotation, bulletTargetRotation, turnSpeed));
    }
    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1.0f);

        isReady = true;
    }

}
