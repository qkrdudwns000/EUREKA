using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    public LayerMask crashMask;
    public Transform myCam;
    public float lookupSpeedX = 10.0f;
    public float lookupSpeedY = 1.0f;
    public float zoomSpeed = 3.0f;
    public float offSet = 0.5f;
    Vector3 curRot = Vector3.zero;
    public Vector2 lookupRange = new Vector2(-60.0f, 80.0f);
    public Vector2 ZoomRange = new Vector2(-8, -1);
    Vector3 camPos = Vector3.zero;
    float desireDistance = 0.0f;

    public GameObject thePlayer;
    [SerializeField]private PlayerController theController;
 
    // Start is called before the first frame update
    void Start()
    {
        curRot.x = transform.localRotation.eulerAngles.x;
        curRot.y = transform.parent.localRotation.eulerAngles.y;

        camPos = myCam.localPosition;

        desireDistance = camPos.z;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        OnCursor();

    }

    private void CameraMovement()
    {
        curRot.x -= Input.GetAxisRaw("Mouse Y") * lookupSpeedY;
        curRot.x = Mathf.Clamp(curRot.x, lookupRange.x, lookupRange.y);

        curRot.y += Input.GetAxisRaw("Mouse X") * lookupSpeedX;

        transform.localRotation = Quaternion.Euler(curRot.x, 0, 0);
        if(theController.isForward)
        {
            thePlayer.transform.localRotation = Quaternion.Lerp(thePlayer.transform.localRotation, Quaternion.Euler(0.0f, curRot.y, 0.0f), Time.deltaTime * 10.0f);
            transform.parent.localRotation = Quaternion.Euler(0, curRot.y, 0);
        }
        else
        {
            transform.parent.localRotation = Quaternion.Euler(0, curRot.y, 0);
        }


        desireDistance += Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed;
        desireDistance = Mathf.Clamp(desireDistance, ZoomRange.x, ZoomRange.y);

        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, -camPos.z + offSet + 0.1f, crashMask))
        {
            camPos.z = -hit.distance + offSet;
        }
        else
        {
            camPos.z = Mathf.Lerp(camPos.z, desireDistance, Time.deltaTime * 3.0f);
        }
        myCam.localPosition = camPos;
    }

    private void OnCursor()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
