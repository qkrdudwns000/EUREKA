using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    public bool DontMove = false;
    float Speed = 2.0f;
    float walkSpeed = 2.0f;
    float runSpeed = 3.0f;
    Vector3 deltaPosition = Vector3.zero;
    Quaternion deltaRotation = Quaternion.identity;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (DontMove) return;

        Speed = walkSpeed;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Speed = runSpeed;
        }

        transform.parent.Translate(deltaPosition, Space.World);
        deltaPosition = Vector3.zero;
        transform.parent.rotation *= deltaRotation;
        deltaRotation = Quaternion.identity;
    }

    private void OnAnimatorMove()
    {
        deltaPosition += GetComponent<Animator>().deltaPosition * Speed;
        deltaRotation *= GetComponent<Animator>().deltaRotation;
    }
}
