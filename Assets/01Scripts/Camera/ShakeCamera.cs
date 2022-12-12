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

    public void OnShakeCamera(float shakeTime = 1.0f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByPosition");
        StartCoroutine("ShakeByPosition");   
    }
    private IEnumerator ShakeByPosition()
    {
        // 흔들기 직전 시작 pos
        Vector3 startPosition = transform.position;

        while (shakeTime > 0.0f)
        {
            // 초기 위치로부터 구 범위(size 1) * shakeIntensity의 범위안에서 카메라 위치변동
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;

            // 시간 감소
            shakeTime -= Time.deltaTime;

            yield return null;
        }
        // 다시 초기의 기억해둔 pos로 이동.
        transform.position = startPosition;
    }
}
