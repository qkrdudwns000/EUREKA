using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MONSTERTYPE
{
    NORMAL, BOSS
}

public class EnemyController : EnemyMovement
{
    public MONSTERTYPE monsterType;
    public int bossID;

    [SerializeField] private SkinnedMeshRenderer theMeshRenderer;
    private Color originColor; // 기본 메터리얼컬러를 저장할변수.
    Vector3 startPos = Vector3.zero; // 시작지점 저장할변수.
    private Transform myTarget;
    [SerializeField]
    private Collider myColider;
    [SerializeField]
    private ResultBgController myResultController;
    [SerializeField]
    private Inventory theInven;
    [SerializeField]
    private QuestManager theQuestManager;

    public Item ingredientItem;

    private float slowFactor = 0.05f;
    private float slowLength = 4f;

    public enum STATE
    {
        Create, Idle, StartPos, Battle, Dead, Result
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
            case STATE.Result:
                StopAllCoroutines();
                ResultReward();
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
                if (myTarget != null)
                {
                    if (!myTarget.GetComponent<PlayerController>().isLive)
                        DeadTarget();
                }
                break;
            case STATE.Dead:
                break;
            case STATE.Result:
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
        if (myState == STATE.Dead || myState == STATE.Result) return;
        myTarget = target;
        StopAllCoroutines();
        ChangeState(STATE.Battle);
    }
    public void LostTarget()
    {
        if (myState == STATE.Dead || myState == STATE.Result) return;
        myTarget = null;
        StopAllCoroutines();
        myAnim.SetBool("IsMoving", false);
        ChangeState(STATE.StartPos);
    }
    public void DeadTarget()
    {
        if (myState == STATE.Dead || myState == STATE.Result) return;
        myTarget = null;
        StopAllCoroutines();
        myAnim.SetBool("IsMoving", false);
        myAnim.SetTrigger("Happy");
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
        if(monsterType == MONSTERTYPE.BOSS)
        StartCoroutine(ChangeResultState());
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
    IEnumerator ChangeResultState()
    {
        yield return new WaitForSeconds(5.0f);

        ChangeState(STATE.Result);
    }
    private void ResultReward()
    {
        GameManager.Inst.Gold += myStat.GetGold;
        GameManager.Inst.levelSystem.AddExperience(myStat.GetExperience);
        if (bossID == theQuestManager.questId && !theQuestManager.questComplete)
        {
            theQuestManager.questActionIndex++;
            theQuestManager.questPopupIndex++;
            theQuestManager.questComplete = true;
        }
        theInven.AcquireItem(ingredientItem, 1);
        myResultController.OpenResult(myStat.GetGold, myStat.GetExperience, ingredientItem);
    }
   
}
