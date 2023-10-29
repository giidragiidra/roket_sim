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

    [SerializeField] private float _fuelAmount;
    [SerializeField] private float _fuelWaste;

    private bool _abilityToFly = true;
    
    private Rigidbody _rb;
    private bool _isStarted = false;
    private ControlMap _controls;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _controls = new ControlMap();
        _controls.InGame.speedscale.performed += ChangeSpeed;
        _controls.InGame.move.performed += SetMove;
        _controls.InGame.dash.performed += Dash;
        _controls.Enable();
    }

    private void Update()
    {
        _moveSpeed = SpeedChangeLimiter(_moveSpeed);
        MovePlayer();
    }

    private void MovePlayer()
    {
        if((_abilityToFly & _isStarted) & _fuelAmount > 0f)
        {
            _rb.AddForce(transform.right * _moveSpeed * -10f, ForceMode.Force);
            FuelWasting();
        }    
    }

    private void Dash(InputAction.CallbackContext context)
    {

    }
    private void FuelWasting()
    {
        _fuelAmount -= Time.deltaTime * _fuelWaste;
    }
    
    private float SpeedChangeLimiter(Single speed)
    {
        return Mathf.Clamp(speed, 0f, _maxChangeValue);
    }
    
    private void SetMove(InputAction.CallbackContext context)
        => _isStarted = true;

    private void ChangeSpeed(InputAction.CallbackContext context)
        => _moveSpeed += (float)(context.ReadValue<Single>() * (_speedChangeModifier/120f));  
}
