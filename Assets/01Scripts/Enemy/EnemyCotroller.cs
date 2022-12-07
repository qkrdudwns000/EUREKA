using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCotroller : CharacterProperty
{
    [SerializeField] private SkinnedMeshRenderer theMeshRenderer;
    private Color originColor;
    // Start is called before the first frame update
    private void Awake()
    {
        originColor = theMeshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(transform.name + "��" + damage + "��ŭ ü���� �����մϴ�.");
        myAnim.SetTrigger("OnHit");
        StartCoroutine("OnHitColor");
    }

    private IEnumerator OnHitColor()
    {
        theMeshRenderer.material.color = Color.red;
        // �¾������ 0.1�ʵ��� ������.
        yield return new WaitForSeconds(0.1f);

        theMeshRenderer.material.color = originColor;
    }
}
