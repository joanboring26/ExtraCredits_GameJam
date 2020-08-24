using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isometricCameraScript : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    private Vector3 offset = new Vector3(0, 50, 500);
    private Camera cam;
    private float positionX;
    private float positionZ;
    private float moveSensitivity = 3.0f;
    private float zoomAmount;
    private float zoomSensitivity = 10.0f;
    bool initialCameraPositionSet = false;
    void Awake()
    {
        transform.SetParent(player.transform);
        cam = GetComponent<Camera>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam.orthographic = true;
        transform.localPosition = offset;
        positionX = transform.localPosition.x;
        positionZ = transform.localPosition.z - 570;
        zoomAmount = cam.orthographicSize;
    }
    void Update()
    {
        cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 6, 20);
        if (!initialCameraPositionSet)
        {
            
            initialCameraPositionSet = true;
        }
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            return;
        }
        SetCameraPosition();
        //-70, -30
        //if(Input.GetKeyDown(KeyCode.E))
        //    transform.localPosition = new Vector3(0,0,0);
    }
    void SetCameraPosition()
    {
        positionX += Input.GetAxis("Mouse X") * moveSensitivity;
        positionZ += Input.GetAxis("Mouse Y") * moveSensitivity;
        positionX = Mathf.Clamp(positionX, -90, 90);
        positionZ = Mathf.Clamp(positionZ, -190, 20);

    }
    void FixedUpdate()
    {
        transform.localPosition = new Vector3(positionX, transform.localPosition.y, positionZ);
        transform.position = new Vector3(transform.position.x, 100, transform.position.z - 87);
    }


}
