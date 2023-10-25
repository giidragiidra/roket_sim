using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotarion : MonoBehaviour
{
    [SerializeField] private float sensX = 1;
    [SerializeField] private float sensY = 1;
    
    [SerializeField] private float startRotx;
    [SerializeField] private float startRoty;
    [SerializeField] private float startRotz;

    private float xRotation;
    private float zRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        xRotation = startRotx;
        zRotation = startRotz;
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
