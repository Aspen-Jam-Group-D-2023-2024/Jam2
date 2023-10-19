using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;
    private float xRotation;
    private float yRotation;
    public Transform playerObj;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // mouse input
        xRotation -= Input.GetAxis("Mouse Y") * sensY;
        yRotation += Input.GetAxis("Mouse X") * sensX;

        // lock y to -90 and 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate camera and orientation
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        
        // rotate player model
        playerObj.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}