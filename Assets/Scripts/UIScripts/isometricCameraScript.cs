using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isometricCameraScript : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    private Vector3 offset = new Vector3(0, 50, -50);
    private Camera cam;
    private float positionX;
    private float positionZ;
    private float moveSensitivity = 3.0f;
    private float zoomAmount;
    private float zoomSensitivity = 10.0f;
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
        positionZ = transform.localPosition.z;
        zoomAmount = cam.orthographicSize;
    }

    void Update()
    {
        positionX += Input.GetAxis("Mouse X") * moveSensitivity;
        positionZ += Input.GetAxis("Mouse Y") * moveSensitivity;
        positionX = Mathf.Clamp(positionX, -20, 20);
        positionZ = Mathf.Clamp(positionZ, -70, -30);
        cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
    }

    void FixedUpdate()
    {
        transform.localPosition = new Vector3(positionX, transform.localPosition.y, positionZ);
    }


}
