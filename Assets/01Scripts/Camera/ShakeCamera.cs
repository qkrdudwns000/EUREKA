using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    static public ShakeCamera inst;
    
    private void Awake()
    {
        inst = this;
    }

    private float shakeTime;
    private float shakeIntensity;
    public bool isShake;

    public void OnShakeCamera(float shakeTime = 0.2f, float shakeIntensity = 0.08f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        isShake = false;
        StopCoroutine("ShakeByPosition");
        StartCoroutine("ShakeByPosition");   
    }
    private IEnumerator ShakeByPosition()
    {
        // ���� ���� ���� pos
        Vector3 startPosition = transform.position;

        while (shakeTime > 0.0f)
        {
            // �ʱ� ��ġ�κ��� �� ����(size 1) * shakeIntensity�� �����ȿ��� ī�޶� ��ġ����
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;

            isShake = true;
            // �ð� ����
            shakeTime -= Time.deltaTime;

            yield return null;
        }
        isShake = false;
        // �ٽ� �ʱ��� ����ص� pos�� �̵�.
        transform.position = startPosition;
    }
}
