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

    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashForse;
    
    private bool _ableToDash = true;

    private bool _ableToFly = true;
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
        if((_ableToFly & _isStarted) & _fuelAmount > 0f)
        {
            _rb.AddForce(transform.right * _moveSpeed * -10f, ForceMode.Force);
            FuelWasting();
        }    
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (!_ableToFly || !_ableToDash)
            return;

        _ableToDash = false;
        //_rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        _rb.AddForce(transform.up * _dashForse, ForceMode.Impulse);
        Invoke(nameof(ResetDash), _dashCooldown);
    }
    
    private void ResetDash()
    {
        _ableToDash = true;
    }
    
    private void FuelWasting()
    {
        _fuelAmount -= Time.deltaTime * _fuelWaste * _moveSpeed;
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
