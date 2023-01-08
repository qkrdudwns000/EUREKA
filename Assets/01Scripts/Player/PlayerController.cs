using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerCharacterProperty
{
    private float currentSpRechargeTime = 0.0f;
    public float spRechargeTime = 0.0f;
    public float spIncreaseSpeed = 1.0f;

    public Vector2 targetDir = Vector2.zero;
    public bool isAutoTarget = false;
    private Vector3 autoTargetDir;
    [SerializeField]
    private Transform CameraArm;
    private SpringArm theSprignArm;
    
    [SerializeField]
    private Transform RealCam;

    private bool isRun = false;
    private float currentSpeed;
    public float smoothness = 10.0f;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float rotaeSpeed = 15.0f;

    public bool isForward = false;
    private bool isCombable = false;
    private bool spUsed = false; // sp 사용중인지 아닌지.
    

    public bool IsCombo = false; // 다른 스크립트에 알리는용도.
    private int clickCount;
    private int staminaCount = 0;

    [SerializeField] private PlayerEquipment theEquipment;
    [SerializeField] private GameManager theManager;
    [SerializeField] private Shop theShop;
    [SerializeField] private QuestNPC theQuest;

    private void Start()
    {
        theSprignArm = CameraArm.GetComponent<SpringArm>();
    }

    // Update is called once per frame
    void Update()
    {
        Interaction();
        if (!Inventory.inventoryActivated && !Shop.isShopping && !SkillSetManager.isSkillSetting
            && !GameManager.isAction && !theSprignArm.isTargetting)
        {
            LookAround();
            if (!myAnim.GetBool("IsComboAttacking") && !myAnim.GetBool("IsHiting") && !SkillSetManager.isSkill)
            {
                PlayerMovement();
                RollingAndBlock();
            }
            if (theEquipment.isEquipWeapon && !SkillSetManager.isSkill)
            {
                ComboAttack();
                CounterAttack();
            }
        }
        else
        {
            myAnim.SetBool("IsWalk", false);
            myAnim.SetBool("IsRun", false);
        }
        SPRechargeTime();
        SPRecover();
    }
    public void Targetting(Transform target)
    {
        autoTargetDir = (target.position - transform.position).normalized;
        isAutoTarget = true;

        CameraArm.rotation = Quaternion.LookRotation(autoTargetDir);
        
    }
    private void AutoTargeting()
    {
        if (isAutoTarget)
        {
            transform.rotation = Quaternion.LookRotation(autoTargetDir);
        }
    }
    private void Interaction()
    {
        if(Input.GetKeyDown(KeyCode.E) && (theShop.isShop || theQuest.isQuest))
        {
            if (theShop.isShop)
                theShop.Interaction();
            else if (theQuest.isQuest)
                theQuest.Interaction();
        }
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            isRun = true;
        else
            isRun = false;


        if (isRun && myStat.SP > Mathf.Epsilon)
        {
            DecreaseStamina(0.5f);
            myAnim.SetBool("IsRun", true);
            currentSpeed = runSpeed;
        }
        else
        {
            myAnim.SetBool("IsRun", false);
            currentSpeed = walkSpeed;
        }


        Vector2 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        bool isMove = moveInput.magnitude != 0;

        if ((moveInput.x < 0.5f && moveInput.x >-0.5f) && moveInput.y > 0.5f)
        {
            isForward = true;
        }
        else
        {
            isForward = false;
        }

        if (isMove)
        {
            Vector3 lookForward = new Vector3(CameraArm.forward.x, 0f, CameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(CameraArm.right.x, 0f, CameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            if (isForward)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookForward),
                    Time.deltaTime * rotaeSpeed);
            else
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir)
                    , Time.deltaTime * rotaeSpeed);


            transform.position += moveDir * Time.deltaTime * currentSpeed;
            myAnim.SetBool("IsWalk", true);
        }
        else
        {
            myAnim.SetBool("IsWalk", false);
        }  
    }
    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector3 camAngle = CameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;
        if(x < 180f)
        {
            x = Mathf.Clamp(x, -20f, 10f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }


        CameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
    public void DecreaseStamina(float _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (myStat.SP > 0.0f)
            myStat.SP -= _count;
        else
            myStat.SP = 0;
    }
    private void SPRechargeTime()
    {
        if(spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
                currentSpRechargeTime++;
            else
                spUsed = false;
        }
    }
    private void SPRecover()
    {
        if(!spUsed && myStat.SP < myStat.maxSP)
        {
            myStat.SP += spIncreaseSpeed;
        }
    }
    public void StaminaControl() // 콤보어택시 animevent에서 호출되는 함수.
    {
        if (myAnim.GetBool("IsComboAttacking") && staminaCount > 0)
        {
            DecreaseStamina(10.0f);
        }
    }
    private void RollingAndBlock()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !myAnim.GetBool("IsRolling") && !myAnim.GetBool("IsHiting") && myStat.SP > 0.0f) 
        {
            DecreaseStamina(15.0f);
            myAnim.SetTrigger("Rolling");
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !myAnim.GetBool("IsBlock") && !myAnim.GetBool("IsRolling") 
            && !myAnim.GetBool("IsHiting") && myStat.SP > 0.0f && theEquipment.isEquipShield)
        {
            myAnim.SetTrigger("Block");
        }
    }
    private void ComboAttack()
    {

        IsCombo = myAnim.GetBool("IsComboAttacking");
        if (Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsComboAttacking") && myStat.SP > 0)
        {
            AutoTargeting();
            myAnim.SetTrigger("ComboAttack");
            ++staminaCount;
        }
        if(isCombable)
        {
            if (Input.GetMouseButton(0))
            {
                ++clickCount;
                ++staminaCount;
            }
        }
    }
    private void CounterAttack()
    {
        if (myAnim.GetBool("IsBlocking") && !myAnim.GetBool("InCounter"))
        {
            if (Input.GetMouseButtonDown(1))
            {
                AutoTargeting();
                DecreaseStamina(10.0f);
                myAnim.SetTrigger("Counter");
            }
        }
    }
    public void ComboCheck(bool v) // 오버로딩 불값들어올때만실행.
    {
        if (v)
        {
            isCombable = true;
            clickCount = 0;
            staminaCount = 0;
        }
        else
        {
            isCombable = false;
            if(clickCount == 0) // 콤보타이밍에 좌클릭안했을경우엔 combofail실행.
            {
                myAnim.SetTrigger("ComboFail");
            }
        }
    }
    public void SkillPlay(Skill _skill)
    {
        AutoTargeting();
        myAnim.SetTrigger(_skill.animeName);
    }
    
    public void Skill_3_AttackCollision()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, 5.0f, 1 << LayerMask.NameToLayer("Enemy"));
        if(_target.Length > 0)
        {
            for(int i = 0; i < _target.Length; i++)
            {
                Debug.Log(_target[i]);
                _target[i].GetComponent<EnemyController>().TakeDamage(20.0f);
            }
        }
    }


    public void TakeDamage(float damage)
    {
        if (!myAnim.GetBool("IsRolling") && !myAnim.GetBool("IsBlock") && !myAnim.GetBool("IsBlcoking") && !myAnim.GetBool("IsCounter")
            && !SkillSetManager.isSkill)
        {
            Debug.Log(transform.name + "가" + damage + "만큼 체력이 감소합니다.");
            myStat.HP -= damage;
            myAnim.SetTrigger("OnHit");
        }
        else if ((myAnim.GetBool("IsBlock") || myAnim.GetBool("IsBlocking")) && !SkillSetManager.isSkill)
        {
            myAnim.ResetTrigger("Blocking");
            myAnim.SetTrigger("Blocking");
        }
    }
}
