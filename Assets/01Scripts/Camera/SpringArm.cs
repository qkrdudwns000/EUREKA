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
        CameraMovement();
        OnCursor();

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
        if (Input.GetKey(KeyCode.LeftAlt) || Inventory.inventoryActivated || Shop.isShopping)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
