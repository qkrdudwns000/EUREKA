using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCotroller : EnemyMovement
{
    [SerializeField] private SkinnedMeshRenderer theMeshRenderer;
    private Color originColor; // �⺻ ���͸����÷��� �����Һ���.
    Vector3 startPos = Vector3.zero; // �������� �����Һ���.
    private Transform myTarget;

    public enum STATE
    {
        Create, Idle, StartPos, Battle, Dead
    }
    public STATE myState = STATE.Create;
    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                break;
            case STATE.StartPos:
                MoveToPosition(startPos, () => ChangeState(STATE.Idle));
                break;
            case STATE.Battle:
                AttackTarget(myTarget);
                break;
            case STATE.Dead:
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                break;
            case STATE.StartPos: 
                break;
            case STATE.Battle:
                break;
            case STATE.Dead:
                break;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        originColor = theMeshRenderer.material.color;
    }
    private void Start()
    {
        startPos = transform.position;
        ChangeState(STATE.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }
    public void FindTarget(Transform target)
    {
        if (myState == STATE.Dead) return;
        myTarget = target;
        StopAllCoroutines();
        ChangeState(STATE.Battle);
    }
    public void LostTarget()
    {
        if (myState == STATE.Dead) return;
        myTarget = null;
        StopAllCoroutines();
        myAnim.SetBool("IsMoving", false);
        ChangeState(STATE.StartPos);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(transform.name + "��" + damage + "��ŭ ü���� �����մϴ�.");
        myStat.HP -= damage;
        myAnim.SetTrigger("OnHit");
        StartCoroutine("OnHitColor");
    }

    private IEnumerator OnHitColor()
    {
        theMeshRenderer.material.color = Color.red;
        // �¾������ 0.1�ʵ��� ������.
        yield return new WaitForSeconds(0.1f);

        theMeshRenderer.material.color = originColor;
    }
}
