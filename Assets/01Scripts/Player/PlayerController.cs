using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerCharacterProperty
{
    public float smoothMoveSpeed = 10.0f;
    public float targettingArea;
    private float currentSpRechargeTime = 0.0f;
    public float spRechargeTime = 0.0f;
    public float spIncreaseSpeed = 1.0f;

    public Vector2 targetDir = Vector2.zero;
    public GameObject theCam;
    private Transform Target; // 타겟팅설정할때 적트랜스폼
    public bool isForward = false;
    private bool isCombable = false;
    private bool spUsed = false; // sp 사용중인지 아닌지.
    [SerializeField] private bool isTargetting = false;

    public bool IsCombo = false; // 다른 스크립트에 알리는용도.
    private int clickCount;
    private int staminaCount = 0;

    [SerializeField] private Transform guardPos;
    [SerializeField] private GameObject guardEffect; // 가드이펙트 프리팹
    [SerializeField] private LayerMask enemyMask; // 적 레이어
    private Material outline; // 아웃라인 쉐이더매터리얼
    [SerializeField] private GameObject theSwordTrail; // 검기 잔상
    [SerializeField] private PlayerEquipment theEquipment;


    Renderer renderers;
    int rendererCount = 0;
    List<Material> materialList = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        outline = new Material(Shader.Find("Draw/OutlineShader"));
    }

    // Update is called once per frame
    void Update()
    {
        SwordTrail();
        if (!Inventory.inventoryActivated)
        {
            FindTargetting();
            if (Input.GetMouseButtonDown(2) && Target != null)
            {
                isTargetting = !isTargetting;
            }
            else if (Target == null)
            {
                isTargetting = false;
            }

            if (isTargetting)
            {
                Targetting();
            }
            Outline();

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

    private void PlayerMovement()
    {
        //shift 키를 안누르면 최대 0.5, shift키를 누르면 최대 1까지 값이바뀜,
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
    private void FindTargetting()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, targettingArea, enemyMask);

        if(_target.Length > 0)
        {
            for(int i = 0; i < _target.Length; i++)
            {
                 if (_target[i].transform.CompareTag("Enemy"))
                 {
                     Target = _target[i].transform;
                 }
            }
        }
        else
        {
            Target = null;
        }
    }
    private void Targetting()
    {
        Vector3 dir = (Target.position - transform.position).normalized;
        if (Target != null && !myAnim.GetBool("IsRolling"))
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
        theCam.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void Outline()
    {
        if (isTargetting && rendererCount == 0)
        {
            rendererCount++;
            renderers = Target.GetComponentInChildren<Renderer>();

            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Add(outline);

            renderers.materials = materialList.ToArray();
        }
        else if(!isTargetting && renderers != null && rendererCount == 1)
        {
            rendererCount = 0;
            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Remove(outline);

            renderers.materials = materialList.ToArray();
        }
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
