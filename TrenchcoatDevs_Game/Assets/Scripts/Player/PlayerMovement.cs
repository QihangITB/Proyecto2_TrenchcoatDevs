using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class PlayerMovement : MonoBehaviour, InputController.IPlayerActions
{
    private InputController _ic;
    private Vector2 _movementP;
    private Rigidbody _rb;

    public float MovementSpeed = 5f;

    private void Awake()
    {
        _ic = new InputController();
        _ic.Player.SetCallbacks(this);
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _ic.Player.Enable();
    }

    private void OnDisable()
    {
        _ic.Player.Disable();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _rb.velocity = new Vector3(_movementP.x * MovementSpeed, _rb.velocity.y, _movementP.y * MovementSpeed);
    }

    void InputController.IPlayerActions.OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _movementP = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _movementP = Vector2.zero;
        }
    }
}
