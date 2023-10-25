using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Single _speedChangeModifier;

    [SerializeField] private Single _maxChangeValue;

    //[SerializeField] private Transform _orientation;
    
    private Rigidbody _rb;

    private ControlMap _controls;
    private Vector3 _moveVector;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _controls = new ControlMap();
        _controls.InGame.speedscale.performed += ChangeSpeed;
        _controls.Enable();
    }

    void Update()
    {
        GetInput();
        MovePlayer();
    }
    private void GetInput()
    {
        var inputVector = _controls.InGame.move.ReadValue<Vector2>();
        _moveVector = transform.forward * inputVector.y + transform.right * inputVector.x;
    }
    private void MovePlayer()
    {
        _rb.AddForce(_moveVector.normalized * _moveSpeed * 10f, ForceMode.Force);
    }
    private Single SpeedChangeLimiter(Single speed)
    {
        if (speed > _speedChangeModifier)
            return speed = _speedChangeModifier;
        return speed;
    }
    private void ChangeSpeed(InputAction.CallbackContext context)
    {
        _moveSpeed += (float)(context.ReadValue<Single>() * SpeedChangeLimiter(_speedChangeModifier/120f));  
    }


}
