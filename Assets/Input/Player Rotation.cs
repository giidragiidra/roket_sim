using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotarion : MonoBehaviour
{
    [SerializeField] private float sensX = 1;
    [SerializeField] private float sensY = 1;

    private float xRotation;
    private float zRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        xRotation = 0;
        zRotation = 0;
    }


    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        xRotation += mouseX;
        zRotation -= mouseY;

        transform.rotation = Quaternion.Euler(0,xRotation,zRotation);
    }
}
