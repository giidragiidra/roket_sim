using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CameraRotarion : MonoBehaviour
{
    [SerializeField] private float sensX = 1;
    [SerializeField] private float sensY = 1;
    
    [SerializeField] private float startRotx;
    [SerializeField] private float startRoty;
    [SerializeField] private float startRotz;

    private float _xRotation;
    private float _zRotation;
    private float _yRotation;
    
    private Quaternion _rotation;

    private ControlMap _controls;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _xRotation = startRotx;
        _yRotation = startRoty;
        _zRotation = startRotz;
        _controls = new ControlMap();
        _controls.Enable();
        
    }


    private void Update()
    {
        MouseRotate();
    }

    private void MouseRotate()
    { 
        var currentRotation = transform.rotation.eulerAngles;

        Vector2 mouseShift = _controls.InGame.mouse.ReadValue<Vector2>() * 0.001f;
        mouseShift.x *= sensX;
        mouseShift.y *= sensY;
        
        currentRotation += new Vector3(0, mouseShift.x, -mouseShift.y);
        
        transform.rotation = Quaternion.Euler(currentRotation);
    
    }

}
