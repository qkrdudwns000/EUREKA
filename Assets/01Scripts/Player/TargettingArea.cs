using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingArea : MonoBehaviour
{
    public float targettingArea;
    private Transform Target; // 타겟팅설정할때 적트랜스폼
    [SerializeField] private bool isTargetting = false;
    [SerializeField] private LayerMask enemyMask; // 적 레이어

    private Material outline; // 아웃라인 쉐이더매터리얼
    Renderer renderers;
    int rendererCount = 0;
    List<Material> materialList = new List<Material>();

    [SerializeField] private PlayerController thePlayer;

    private void Update()
    {
        if(!Inventory.inventoryActivated)
        {
            FindTargetting();
            if (Input.GetMouseButtonDown(2) && Target != null)
            {
                isTargetting = !isTargetting;
            }
            else if (Target == null)
            {
                isTargetting = false;
            }

            if (isTargetting)
            {
                thePlayer.Targetting(Target);
            }
            if(!isTargetting)
            {
                thePlayer.isAutoTarget = false;
            }
            Outline();
        }
    }
    void Start()
    {
        outline = new Material(Shader.Find("Draw/OutlineShader"));
    }
    private void FindTargetting()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, targettingArea, enemyMask);

        if (_target.Length > 0)
        {
            for (int i = 0; i < _target.Length; i++)
            {
                if (_target[i].transform.CompareTag("Enemy"))
                {
                    Target = _target[i].transform;
                }
            }
        }
        else
        {
            Target = null;
        }
    }

    private void Outline()
    {
        if (isTargetting && rendererCount == 0)
        {
            rendererCount++;
            renderers = Target.GetComponentInChildren<Renderer>();

            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Add(outline);

            renderers.materials = materialList.ToArray();
        }
        else if (!isTargetting && renderers != null && rendererCount == 1)
        {
            rendererCount = 0;
            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Remove(outline);

            renderers.materials = materialList.ToArray();
        }
    }
}
