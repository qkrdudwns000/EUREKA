using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterProperty
{
    public float smoothMoveSpeed = 10.0f;
    public Vector2 targetDir = Vector2.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Rolling();
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
        
    }
    private void Rolling()
    {
        if (Input.GetKeyDown(KeyCode.Space) && targetDir != Vector2.zero && !myAnim.GetBool("IsRolling")) 
        {
            if (targetDir.y < 0.0f)
            {
                transform.Rotate(Vector3.up * 180.0f);
                myAnim.SetTrigger("Rolling");
            }
            //myAnim.SetTrigger("Rolling");
        }
    }
}
