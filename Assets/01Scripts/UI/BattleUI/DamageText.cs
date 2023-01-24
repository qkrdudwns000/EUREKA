using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    private TMPro.TMP_Text damageText;
    private GameObject theCam;
    private Color alpha;
    public float damageFlow;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 1.0f;
        destroyTime = 1.3f;
        theCam = GameObject.Find("Main Camera");
        damageText = GetComponent<TMPro.TextMeshPro>();
        //alpha = damageText.color;
        damageText.text = damageFlow.ToString();
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); //텍스트 위치이동.
        transform.LookAt(theCam.transform);
        transform.Rotate(0, 180, 0);

        //alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값변화
        //damageText.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
