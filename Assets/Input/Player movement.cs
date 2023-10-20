using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _orientation;
    private Rigidbody _rb;

    private ControlMap _controls;
    private Vector3 _moveVector;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _controls = new ControlMap();
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
}
