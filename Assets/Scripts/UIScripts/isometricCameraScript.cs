﻿using System.Collections;
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
        positionX = Mathf.Clamp(positionX, -90, 90);
        //-70, -30
        positionZ = Mathf.Clamp(positionZ, -120, 20);
        cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 6, 20);
        //if(Input.GetKeyDown(KeyCode.E))
        //    transform.localPosition = new Vector3(0,0,0);
    }

    void FixedUpdate()
    {
        transform.localPosition = new Vector3(positionX, transform.localPosition.y, positionZ);
        transform.position = new Vector3(transform.position.x, 100, transform.position.z - 87);
    }


}
