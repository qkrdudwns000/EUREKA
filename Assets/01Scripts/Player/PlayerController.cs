using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerCharacterProperty
{
    public float smoothMoveSpeed = 10.0f;
    private float currentSpRechargeTime = 0.0f;
    public float spRechargeTime = 0.0f;
    public float spIncreaseSpeed = 1.0f;

    public Vector2 targetDir = Vector2.zero;
    public GameObject theCam;

    private bool isRun = false;
    private float currentSpeed;
    private Camera _camera;
    public float smoothness = 10.0f;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    CharacterController _controller;

    public bool isForward = false;
    private bool isCombable = false;
    private bool spUsed = false; // sp 사용중인지 아닌지.
    

    public bool IsCombo = false; // 다른 스크립트에 알리는용도.
    private int clickCount;
    private int staminaCount = 0;

    [SerializeField] private Transform guardPos;
    [SerializeField] private GameObject guardEffect; // 가드이펙트 프리팹

    [SerializeField] private GameObject theSwordTrail; // 검기 잔상
    [SerializeField] private PlayerEquipment theEquipment;
    [SerializeField] private Shop theShop;


    // Start is called before the first frame update
    private void Start()
    {
        _controller = this.GetComponent<CharacterController>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Interaction();
        SwordTrail();
        if (!Inventory.inventoryActivated && !Shop.isShopping)
        {
            if (!myAnim.GetBool("IsComboAttacking"))
            {
                PlayerMovement();
                RollingAndBlock();
            }
            if (theEquipment.isEquipWeapon)
            {
                ComboAttack();
                CounterAttack();
            }
            
        }
        SPRechargeTime();
        SPRecover();
    }
    private void LateUpdate()
    {
        PlayerRotate();
    }
    public void Targetting(Transform target)
    {
        Vector3 dir = (target.position - transform.position).normalized;
        if (target != null && !myAnim.GetBool("IsRolling"))
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
        theCam.transform.rotation = Quaternion.LookRotation(dir);
    }
    private void Interaction()
    {
        if(Input.GetKeyDown(KeyCode.E) && theShop.isShop)
        {
            theShop.Interaction();
        }
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            isRun = true;
        else
            isRun = false;

        currentSpeed = (isRun) ? runSpeed : walkSpeed;

        if(isRun)
        {
            DecreaseStamina(0.5f);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

        _controller.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);

        float percent = ((isRun) ? 1 : 0.5f) * moveDirection.magnitude;
        myAnim.SetFloat("x", percent, 0.1f, Time.deltaTime);

        if (Input.GetKey(KeyCode.W) && !myAnim.GetBool("IsRolling"))
        {
            isForward = true;
        }
        else
        {
            isForward = false;
        }
    }
    private void PlayerRotate()
    {
        if (isForward)
        {
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
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
    public void StaminaControl() // 콤보어택시 animevent에서 호출되는 함수.
    {
        if (myAnim.GetBool("IsComboAttacking") && staminaCount > 0)
        {
            DecreaseStamina(10.0f);
        }
    }
    private void RollingAndBlock()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && !myAnim.GetBool("IsRolling") && !myAnim.GetBool("IsHiting") && myStat.SP > 0.0f) 
        {
            dir.Normalize();

            transform.rotation = Quaternion.LookRotation(theCam.transform.rotation * dir);
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
            //transform.rotation = Quaternion.LookRotation(theCam.transform.forward);
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
    public void RollingEnd()
    {
        transform.rotation = Quaternion.LookRotation(theCam.transform.forward); // 구르기 후 정면주시를위함.
    }
    


    public void TakeDamage(float damage)
    {
          if (!myAnim.GetBool("IsRolling") && !myAnim.GetBool("IsBlock") && !myAnim.GetBool("IsBlcoking") && !myAnim.GetBool("IsCounter"))
        {
            Debug.Log(transform.name + "가" + damage + "만큼 체력이 감소합니다.");
            myStat.HP -= damage;
            myAnim.SetTrigger("OnHit");
        }
        else if (myAnim.GetBool("IsBlock"))
        {
            myAnim.SetTrigger("Blocking");
            EffectCase("Guard");
        }
        
    }

    private void EffectCase(string s)
    {
        switch (s)
        {
            case "Guard":
                Instantiate(guardEffect, guardPos.transform.position, Quaternion.identity);
                ShakeCamera.inst.OnShakeCamera(0.3f, 0.1f);
                break;
        }
    }
    private void SwordTrail()
    {
        if (myAnim.GetBool("IsComboAttacking"))
        {
            theSwordTrail.SetActive(true);
        }
        else if (myAnim.GetBool("IsCounter"))
        {
            //theSwordTtrail.GetComponent<TrailRenderer>().startColor = 
            theSwordTrail.SetActive(true);
        }
        else
        {
            theSwordTrail.SetActive(false);
        }
    }
}
