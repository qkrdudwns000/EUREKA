using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : PlayerCharacterProperty
{
    private float currentSpRechargeTime = 0.0f;
    public float spRechargeTime = 0.0f;
    public float spIncreaseSpeed = 3.0f;

    public Vector2 targetDir = Vector2.zero;
    public bool isAutoTarget = false;
    private Vector3 autoTargetDir;
    [SerializeField]
    private Transform CameraArm;
    private SpringArm theSprignArm;
    [SerializeField]
    private Animator StatusAnim;
    private Collider myColider;
    private Rigidbody myRigid;
    
    [SerializeField]
    private Transform RealCam;
    [SerializeField]
    private Image bloodScreen;

    private bool isRun = false;
    private float currentSpeed;
    public float smoothness = 10.0f;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float rotaeSpeed = 15.0f;

    public bool isForward = false;
    private bool isCombable = false;
    private bool spUsed = false; // sp ��������� �ƴ���.
    public bool isLive = true;

    

    public bool IsCombo = false; // �ٸ� ��ũ��Ʈ�� �˸��¿뵵.
    private int clickCount;
    private int staminaCount = 0;

    [SerializeField] private PlayerEquipment theEquipment;
    [SerializeField] private GameManager theManager;
    [SerializeField] private Shop theShop;
    [SerializeField] private QuestNPC theQuest;
    [SerializeField] private ResultBgController myResultController;

    private void Start()
    {
        myColider = GetComponent<Collider>();
        isLive = true;
        theSprignArm = CameraArm.GetComponent<SpringArm>();
        myRigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //RayCastCheck();
        if (isLive)
        {
            SPRechargeTime();
            SPRecover();

            if (!Inventory.inventoryActivated && !Shop.isShopping && !SkillSetManager.isSkillSetting
                && !GameManager.isAction && !theSprignArm.isTargetting && !GameManager.isPause)
            {
                if (!myAnim.GetBool("WinMotion"))
                {
                    if (!myAnim.GetBool("IsComboAttacking") && !myAnim.GetBool("IsHiting") && !SkillSetManager.isSkill)
                    {
                        PlayerMovement();
                        //RollingAndBlock();
                    }
                    
                }
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            Interaction();
            if (!Inventory.inventoryActivated && !Shop.isShopping && !SkillSetManager.isSkillSetting
                && !GameManager.isAction && !theSprignArm.isTargetting && !GameManager.isPause)
            {
                if(!theSprignArm.isOnCusor)
                LookAround();
                if (!myAnim.GetBool("WinMotion"))
                {
                    if (!myAnim.GetBool("IsComboAttacking") && !myAnim.GetBool("IsHiting") && !SkillSetManager.isSkill)
                    {

                        RollingAndBlock();
                    }
                    if (theEquipment.isEquipWeapon && !SkillSetManager.isSkill && !MapZone.isWatchingMap)
                    {
                        ComboAttack();
                        CounterAttack();
                    }
                }
            }
            else
            {

                myAnim.SetBool("IsWalk", false);
                myAnim.SetBool("IsRun", false);

                myAnim.SetBool("IsWalk2", false);
                myAnim.SetBool("IsRun2", false);
            }
        }  
    }
    public void Targetting(Transform target)
    {
        autoTargetDir = (target.position - transform.position).normalized;
        isAutoTarget = true;

        if(!ShakeCamera.inst.isShake)
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
            if(theEquipment.isEquipWeapon)
                myAnim.SetBool("IsRun", true);
            else
                myAnim.SetBool("IsRun2", true);


            currentSpeed = runSpeed;
        }
        else
        {
            if(theEquipment.isEquipWeapon)
                myAnim.SetBool("IsRun", false);
            else
                myAnim.SetBool("IsRun2", false);

            currentSpeed = walkSpeed;
        }
        if (Physics.Raycast(transform.position + transform.up, transform.forward, myColider.bounds.extents.x + 0.1f, 1 << LayerMask.NameToLayer("Wall")))
        {
            currentSpeed = 0;
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


            //transform.position += moveDir.normalized * Time.deltaTime * currentSpeed;
            Vector3 velocity = moveDir.normalized * currentSpeed;
            myRigid.MovePosition(transform.position + velocity * Time.deltaTime);

            if (theEquipment.isEquipWeapon)
                myAnim.SetBool("IsWalk", true);
            else
                myAnim.SetBool("IsWalk2", true);
        }
        else
        {

             myAnim.SetBool("IsWalk", false);
             myAnim.SetBool("IsRun", false);

             myAnim.SetBool("IsWalk2", false);
             myAnim.SetBool("IsRun2", false);
        }  
    }
    private void LookAround()
    {
        if (!MapZone.isWatchingMap)
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector3 camAngle = CameraArm.rotation.eulerAngles;
            float x = camAngle.x - mouseDelta.y;
            if (x < 180f)
            {
                x = Mathf.Clamp(x, -20f, 10f);
            }
            else
            {
                x = Mathf.Clamp(x, 335f, 361f);
            }


            CameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
        }
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
    public void StaminaControl() // �޺����ý� animevent���� ȣ��Ǵ� �Լ�.
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
           

            int rndVoice = Random.Range(0, 2);
            if(rndVoice == 0)
                SoundManager.inst.SFXPlay("Rolling1");
            else
                SoundManager.inst.SFXPlay("Rolling2");

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
    public void ComboCheck(bool v) // �����ε� �Ұ����ö�������.
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
            if(clickCount == 0) // �޺�Ÿ�ֿ̹� ��Ŭ����������쿣 combofail����.
            {
                myAnim.SetTrigger("ComboFail");
            }
        }
    }
    public void SkillPlay(Skill _skill)
    {
        AutoTargeting();
        DecreaseStamina(20.0f);
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
                _target[i].GetComponent<EnemyController>().TakeDamage(myStat.AP * 2);
            }
        }
    }


    public void TakeDamage(float damage)
    {
        if (!myAnim.GetBool("IsRolling") && !myAnim.GetBool("IsBlock") && !myAnim.GetBool("IsBlcoking") && !myAnim.GetBool("IsCounter")
            && !SkillSetManager.isSkill)
        {
            damage -= myStat.DP;

            Debug.Log(transform.name + "��" + damage + "��ŭ ü���� �����մϴ�.");
            if(myStat.HP - damage > 0.0f)
            {
                myStat.HP -= damage;
                myAnim.SetTrigger("OnHit");
                StopCoroutine(ShowBloodScreen());
                StartCoroutine(ShowBloodScreen());
                int rndVoice = Random.Range(0, 2);
                switch(rndVoice)
                {
                    case 0:
                        SoundManager.inst.SFXPlay("Hurt1");
                        break;
                    case 1:
                        SoundManager.inst.SFXPlay("Hurt2");
                        break;
                }
            }
            else
            {
                if (isLive)
                {
                    myStat.HP -= damage;
                    DeadPlayer();
                }
            }
            StatusAnim.SetTrigger("Shake");   
        }
        else if ((myAnim.GetBool("IsBlock") || myAnim.GetBool("IsBlocking")) && !SkillSetManager.isSkill)
        {
            ShakeCamera.inst.OnShakeCamera();
            AutoTargeting();
            myAnim.ResetTrigger("Blocking");
            myAnim.SetTrigger("Blocking");
            SoundManager.inst.SFXPlay("Blocking");
        }
    }
    public void DeadPlayer()
    {
        StopAllCoroutines();
        isLive = false;

        myAnim.SetBool("IsWalk", false);
        myAnim.SetBool("IsRun", false);
        myAnim.SetBool("IsWalk2", false);
        myAnim.SetBool("IsRun2", false);

        myAnim.SetTrigger("Dead");
        SoundManager.inst.SFXPlay("Death");

        StartCoroutine(ChangeResultState());
    }
    IEnumerator ChangeResultState()
    {
        yield return new WaitForSeconds(3.0f);

        ResultReward();
    }
    private void ResultReward()
    {
        StopAllCoroutines();
        myResultController.OpenResult();
    }
    public void WinMotion()
    {
        myAnim.SetTrigger("Win");
    }
   
    IEnumerator ShowBloodScreen()
    {
        bloodScreen.color = new Color(1, 0, 0, Random.Range(0.4f, 0.5f));
        yield return new WaitForSeconds(0.2f);
        bloodScreen.color = Color.clear;
    }
}
