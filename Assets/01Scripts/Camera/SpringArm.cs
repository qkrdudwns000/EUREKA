using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    public LayerMask crashMask;
    public Transform objectTofollow;
    public float followSpeed = 10.0f;
    public float sensitivity = 100.0f;
    public Vector2 clampAngle = new Vector2(70.0f, -10.0f);

    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    public float finalDistace;
    public float smoothness;
    
    [SerializeField]private PlayerController theController;
 
    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistace = realCamera.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Inventory.inventoryActivated && !Shop.isShopping)
        {
            CameraMovement();
        }
        OnCursor();

    }

    private void CameraMovement()
    {
        rotX += Input.GetAxis("Mouse Y") * -sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle.x, clampAngle.y);

        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;

        //if (theController.isForward)
        //{
        //    thePlayer.transform.localRotation = Quaternion.Lerp(thePlayer.transform.localRotation, Quaternion.Euler(0.0f, curRot.y, 0.0f), Time.deltaTime * 10.0f);
        //    transform.parent.localRotation = Quaternion.Euler(0, curRot.y, 0);
        //}
        //else
        //{
        //    transform.parent.localRotation = Quaternion.Euler(0, curRot.y, 0);
        //}
    }
    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;
        if(Physics.Linecast(transform.position, finalDir, out hit, crashMask))
        {
            finalDistace = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistace = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistace, Time.deltaTime * smoothness);
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
