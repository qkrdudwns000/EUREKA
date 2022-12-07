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
    private int clickCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        RollingAndBlock();
        ComboAttack();
    }

    private void PlayerMovement()
    {
        //shift 키를 안누르면 최대 0.5, shift키를 누르면 최대 1까지 값이바뀜,
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
        if (Input.GetKey(KeyCode.Space) && targetDir != Vector2.zero && !myAnim.GetBool("IsRolling")) 
        {
            dir.Normalize();

            transform.rotation = Quaternion.LookRotation(theCam.transform.rotation * dir);

            myAnim.SetTrigger("Rolling");
            
        }
        else if (Input.GetKey(KeyCode.Space) && -0.2 < targetDir.x && 0.2 > targetDir.x && -0.2 < targetDir.y && 0.2 > targetDir.y
            && !myAnim.GetBool("IsBlock") && !myAnim.GetBool("IsRolling"))
        {
            myAnim.SetTrigger("Block");
        }
    }
    private void ComboAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
    public void ComboCheck(bool v)
    {
        if (v)
        {
            IsCombable = true;
            clickCount = 0;
        }
        else
        {
            IsCombable = false;
            if(clickCount == 0)
            {
                myAnim.SetTrigger("ComboFail");
            }
        }
    }
}
