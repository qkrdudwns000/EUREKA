using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : CharacterProperty
{
    // Start is called before the first frame update

    Coroutine moveCo = null;
    Coroutine rotCo = null;
    Coroutine attackCo = null;

    protected void AttackTarget(Transform target)
    {
        StopAllCoroutines();
        attackCo = StartCoroutine(AttackingTarget(target, myStat.AttackRange, myStat.AttackDelay));
    }
    protected void MoveToPosition(Vector3 pos, UnityAction done = null, bool Rot = true)
    {
        if (attackCo != null)
        {
            StopCoroutine(attackCo);
            attackCo = null;
        }
        if(moveCo != null)
        {
            StopCoroutine(moveCo);
            moveCo = null;
        }
        moveCo = StartCoroutine(MovingToPosition(pos, done));

        if (Rot)
        {
            if(rotCo != null)
            {
                StopCoroutine(rotCo);
                rotCo = null;
            }
            rotCo = StartCoroutine(RotatingToPosition(pos));
        }
    }

    IEnumerator MovingToPosition(Vector3 pos, UnityAction done)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        myAnim.SetBool("IsMoving", true);
        while (dist > 0.0f)
        {
            if (myAnim.GetBool("IsAttacking"))
            {
                myAnim.SetBool("IsMoving", false);
                yield break;
            }

            if (!myAnim.GetBool("IsAttacking"))
            {
                float delta = myStat.MoveSpeed * Time.deltaTime;
                if(delta > dist)
                {
                    delta = dist;
                }
                dist -= delta;
                transform.Translate(dir * delta, Space.World);
            }
            yield return null;
        }
        myAnim.SetBool("IsMoving", false);
        done?.Invoke();
    }
    IEnumerator RotatingToPosition(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized;
        float Angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -rotDir;
        }
        while (Angle > 0.0f)
        {
            if (!myAnim.GetBool("IsAttacking"))
            {
                float delta = myStat.RotSpeed * Time.deltaTime;
                if (delta > Angle)
                {
                    delta = Angle;
                }
                Angle -= delta;
                transform.Rotate(Vector3.up * rotDir * delta, Space.World);
            }
            yield return null;
        }
    }
    IEnumerator AttackingTarget(Transform target, float AttackRange, float AttackDelay)
    {
        float playTime = 0.0f;
        float delta = 0.0f;
        while(target != null)
        {
            if (!myAnim.GetBool("IsAttacking"))
                playTime += Time.deltaTime;

            Vector3 dir = target.position - transform.position; // �÷��̾�����ϴ� ����
            float dist = dir.magnitude;
            dir.Normalize();
            if (dist > AttackRange && !myAnim.GetBool("IsAttacking"))
            {
                myAnim.SetBool("IsMoving", true);
                delta = myStat.MoveSpeed * Time.deltaTime;
                if(delta > dist)
                {
                    delta = dist;
                }
                transform.Translate(dir * delta, Space.World);
            }
            else
            {
                myAnim.SetBool("IsMoving", false);
                if(playTime >= AttackDelay) // ������ �����Ǹ� ����.
                {
                    playTime = 0.0f;
                    myAnim.SetTrigger("Attack1");
                }
            }
            //ȸ��
            if (!myAnim.GetBool("IsAttacking"))
            {
                delta = myStat.RotSpeed * Time.deltaTime;
                float Angle = Vector3.Angle(dir, transform.forward);
                float rotDir = 1.0f;
                if (Vector3.Dot(transform.right, dir) < 0.0f)
                {
                    rotDir = -rotDir;
                }
                if (delta > Angle)
                {
                    delta = Angle;
                }
                transform.Rotate(Vector3.up * delta * rotDir, Space.World);
            }
            yield return null;
        }
        myAnim.SetBool("IsMoving", false);
    }
}
