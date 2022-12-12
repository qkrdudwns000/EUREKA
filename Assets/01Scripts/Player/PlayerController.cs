using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterProperty
{
    public float smoothMoveSpeed = 10.0f;
    public Vector2 targetDir = Vector2.zero;
    public GameObject theCam;
    public bool isForward = false;
    private bool IsCombable = false;
    public bool IsCombo = false; // �ٸ� ��ũ��Ʈ�� �˸��¿뵵.
    private int clickCount;

    [SerializeField] private Transform guardPos;
    [SerializeField] private GameObject guardEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!myAnim.GetBool("IsComboAttacking"))
        {
            PlayerMovement();
            RollingAndBlock();
        }
        ComboAttack();
        if (myAnim.GetBool("IsBlocking") && !myAnim.GetBool("InCounter"))
        {
            if (Input.GetMouseButtonDown(1))
            {
                myAnim.SetTrigger("Counter");
            }
        }
    }

    private void PlayerMovement()
    {
        //shift Ű�� �ȴ����� �ִ� 0.5, shiftŰ�� ������ �ִ� 1���� ���̹ٲ�,
        float offSet = 0.5f + Input.GetAxis("Sprint") * 0.5f;
        targetDir.x = Input.GetAxis("Horizontal") * offSet;
        targetDir.y = Input.GetAxis("Vertical") * offSet;

        float x = Mathf.Lerp(myAnim.GetFloat("x"), targetDir.x, Time.deltaTime * smoothMoveSpeed);
        float y = Mathf.Lerp(myAnim.GetFloat("y"), targetDir.y, Time.deltaTime * smoothMoveSpeed);

        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);



        if (y > 0.1f && !myAnim.GetBool("IsRolling")) 
        {
            isForward = true;
        }
        else
        {
            isForward = false;
        }
        
    }
    private void RollingAndBlock()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && !myAnim.GetBool("IsRolling") && !myAnim.GetBool("IsHiting")) 
        {
            dir.Normalize();

            transform.rotation = Quaternion.LookRotation(theCam.transform.rotation * dir);

            myAnim.SetTrigger("Rolling");
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !myAnim.GetBool("IsBlock") && !myAnim.GetBool("IsRolling") && !myAnim.GetBool("IsHiting"))
        {
            myAnim.SetTrigger("Block");
        }
    }
    private void ComboAttack()
    {
        IsCombo = myAnim.GetBool("IsComboAttacking");
        if (Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsComboAttacking"))
        {
            //transform.rotation = Quaternion.LookRotation(theCam.transform.forward);
            myAnim.SetTrigger("ComboAttack");
        }
        if(IsCombable)
        {
            if (Input.GetMouseButton(0))
            {
                clickCount++;
            }
        }
    }
    public void ComboCheck(bool v) // �����ε� �Ұ����ö�������.
    {
        if (v)
        {
            IsCombable = true;
            clickCount = 0;
        }
        else
        {
            IsCombable = false;
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
    public void TakeDamage(int damage)
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
}
