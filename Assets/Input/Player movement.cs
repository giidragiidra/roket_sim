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

    [SerializeField] private float _fuelLeft;
    [SerializeField] private float _fuelWaste;

    private bool _abilityToFly = true;
    
    private Rigidbody _rb;
    private bool _isStarted = false;
    private ControlMap _controls;
    private Vector3 _moveVector;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _controls = new ControlMap();
        _controls.InGame.speedscale.performed += ChangeSpeed;
        _controls.InGame.move.performed += SetMove;
        _controls.Enable();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if(_abilityToFly & _isStarted)
        {
            _rb.AddForce(transform.right * _moveSpeed * -10f, ForceMode.Force);
        }    
        
    }

    private float SpeedChangeLimiter(Single speed)
        => Mathf.Clamp(speed, -_maxChangeValue, _maxChangeValue);

    private void SetMove(InputAction.CallbackContext context)
        => _isStarted = true;

    private void ChangeSpeed(InputAction.CallbackContext context)
        => _moveSpeed += (float)(context.ReadValue<Single>() * SpeedChangeLimiter(_speedChangeModifier)/120f);  
}
