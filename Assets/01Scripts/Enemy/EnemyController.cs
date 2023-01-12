using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyController : EnemyMovement
{
    [SerializeField] private SkinnedMeshRenderer theMeshRenderer;
    private Color originColor; // 기본 메터리얼컬러를 저장할변수.
    Vector3 startPos = Vector3.zero; // 시작지점 저장할변수.
    private Transform myTarget;
    [SerializeField]
    private Collider myColider;

    private float slowFactor = 0.05f;
    private float slowLength = 4f;

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
                DeadMonster();
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
        myColider = GetComponent<Collider>();
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
        SlowMotion();
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

    public void TakeDamage(float damage)
    {
        Debug.Log(transform.name + "가" + damage + "만큼 체력이 감소합니다.");
        if (myStat.HP - damage > 0.0f)
        {
            myStat.HP -= damage;
            if (!myAnim.GetBool("IsAttacking"))
            {
                myAnim.SetTrigger("OnHit");
            }
        }
        else
        {
            myStat.HP -= damage;
            ChangeState(STATE.Dead);
        }

        StartCoroutine("OnHitColor");
    }

    private IEnumerator OnHitColor()
    {
        theMeshRenderer.material.color = Color.red;
        // 맞았을경우 0.1초동안 색변경.
        yield return new WaitForSeconds(0.3f);

        theMeshRenderer.material.color = originColor;
    }
    public void DeadMonster()
    {
        StopAllCoroutines();
        myAnim.SetBool("IsMoving", false);
        myAnim.SetTrigger("Dead");
        myTarget = null;
        myColider.enabled = false;
        DoSlowMotion();
    }
    private void DoSlowMotion()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    private void SlowMotion()
    {
        Time.timeScale += (1f / slowLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
