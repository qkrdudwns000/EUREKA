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
        Debug.Log(transform.name + "가" + damage + "만큼 체력이 감소합니다.");
        myAnim.SetTrigger("OnHit");
        StartCoroutine("OnHitColor");
    }

    private IEnumerator OnHitColor()
    {
        theMeshRenderer.material.color = Color.red;
        // 맞았을경우 0.1초동안 색변경.
        yield return new WaitForSeconds(0.1f);

        theMeshRenderer.material.color = originColor;
    }
}
