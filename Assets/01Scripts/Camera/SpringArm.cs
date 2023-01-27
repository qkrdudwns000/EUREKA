using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    public LayerMask crashMask;
    public Transform myCam;
    public float ZoomSpeed = 3.0f;
    public float Offset = 0.5f;
    public Vector2 ZoomRange = new Vector2(-5, -2);

    Vector3 camPos = Vector3.zero;
    float desireDistance = 0.0f;

    private Vector3 orgPos;
    private Quaternion orgRot;
    [HideInInspector]
    public bool isTargetting = false;

    [SerializeField]
    private PlayerController theController;
    [SerializeField]
    private Transform thePlayer;
 
    // Start is called before the first frame update
    void Start()
    {
        camPos = myCam.localPosition;

        desireDistance = camPos.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isTargetting)
        {
            CameraMovement();
            OnCursor();
        }
    }

    private void CameraMovement()
    {
        desireDistance += Input.GetAxisRaw("Mouse ScrollWheel") * ZoomSpeed;
        desireDistance = Mathf.Clamp(desireDistance, ZoomRange.x, ZoomRange.y);

        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, -camPos.z + Offset + 0.1f, crashMask))
        {
            camPos.z = -hit.distance + Offset;
        }
        else
        {
            camPos.z = Mathf.Lerp(camPos.z, desireDistance, Time.deltaTime * 3.0f);
        }
        myCam.localPosition = camPos;
    }
    private void OnCursor()
    {
        if (Input.GetKey(KeyCode.LeftAlt) || Inventory.inventoryActivated || Shop.isShopping
            || SkillSetManager.isSkillSetting || MapZone.isWatchingMap || GameManager.isPause || ResultBgController.isResulting)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void CameraOriginSetting()
    {
        orgPos = transform.position;
        orgRot = transform.rotation;
    }

    public void CameraTargeting(Transform _target, float _camSpeed = 0.2f)
    {
        StopAllCoroutines();
        if (!isTargetting)
            StartCoroutine(CameraTargetingCo(_target, _camSpeed));
        else
        {
            StartCoroutine(OriginTargetingCo());
        }
    }

    private IEnumerator CameraTargetingCo(Transform _target, float _camSpeed = 0.2f)
    {
        Vector3 _targetPos = _target.GetComponent<ObjData>().targetingPos.position;
        Vector3 _targetFrontPos = _targetPos + _target.GetComponent<ObjData>().targetingPos.forward;
        Vector3 _Direction = (_targetPos - _targetFrontPos).normalized;
        isTargetting = true;

        
        while (transform.position != _targetFrontPos || Quaternion.Angle(transform.rotation,Quaternion.LookRotation(_Direction)) >= 0.5f)
        {
            transform.position = _targetFrontPos;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_Direction), _camSpeed);
            myCam.localPosition = Vector3.zero;
            yield return null;
        }
    }
    private IEnumerator OriginTargetingCo(float _camSpeed = 0.2f)
    {
        while (transform.position != orgPos || Quaternion.Angle(transform.rotation, orgRot) >= 1.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, orgPos, _camSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, orgRot, _camSpeed);
            myCam.localPosition = new Vector3(0, 0, -3);
            yield return null;
        }
        transform.position = orgPos;
        isTargetting = false;

    }
    
}
