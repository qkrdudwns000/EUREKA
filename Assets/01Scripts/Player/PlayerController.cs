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
    
    public bool isForward = false;
    private bool isCombable = false;
    private bool spUsed = false; // sp ��������� �ƴ���.
    

    public bool IsCombo = false; // �ٸ� ��ũ��Ʈ�� �˸��¿뵵.
    private int clickCount;
    private int staminaCount = 0;

    [SerializeField] private Transform guardPos;
    [SerializeField] private GameObject guardEffect; // ��������Ʈ ������

    [SerializeField] private GameObject theSwordTrail; // �˱� �ܻ�
    [SerializeField] private PlayerEquipment theEquipment;
    [SerializeField] private Shop theShop;


    // Start is called before the first frame update
    

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
        //shift Ű�� �ȴ����� �ִ� 0.5, shiftŰ�� ������ �ִ� 1���� ���̹ٲ�,
        float offSet = 0.5f;
        if (myStat.SP > 0.0f)
        {
            offSet += Input.GetAxis("Sprint") * 0.5f;
        }
        
        targetDir.x = Input.GetAxis("Horizontal") * offSet;
        targetDir.y = Input.GetAxis("Vertical") * offSet;

        float x = Mathf.Lerp(myAnim.GetFloat("x"), targetDir.x, Time.deltaTime * smoothMoveSpeed);
        float y = Mathf.Lerp(myAnim.GetFloat("y"), targetDir.y, Time.deltaTime * smoothMoveSpeed);

        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);

        if (offSet > 0.6f)
        {
            DecreaseStamina(0.5f);
        }

        if (y > 0.1f && !myAnim.GetBool("IsRolling"))
        {
            isForward = true;
        }
        else
        {
            isForward = false;
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
    public void RollingEnd()
    {
        transform.rotation = Quaternion.LookRotation(theCam.transform.forward); // ������ �� �����ֽø�����.
    }
    


    public void TakeDamage(float damage)
    {
          if (!myAnim.GetBool("IsRolling") && !myAnim.GetBool("IsBlock") && !myAnim.GetBool("IsBlcoking") && !myAnim.GetBool("IsCounter"))
        {
            Debug.Log(transform.name + "��" + damage + "��ŭ ü���� �����մϴ�.");
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
