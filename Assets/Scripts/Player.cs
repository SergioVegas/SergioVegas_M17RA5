using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MoveBehavior))]
public  class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private Rigidbody _rb;
    private CharacterController _controller;
    protected MoveBehavior _mb;
    private InputSystem_Actions _actions;
    protected float speed = 2;
    private float xVelocity;
    private float zVelocity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        _mb = GetComponent<MoveBehavior>();
        _actions = new InputSystem_Actions();
        _actions.Player.SetCallbacks(this);
    }

    void Update()
    {
        _mb.MoveCharacter(new Vector3(xVelocity, 0,zVelocity), speed);
        _mb.RotateCharacter(new Vector3(xVelocity, 0, zVelocity));
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>(); 
        xVelocity = input.x;
        zVelocity = input.y;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }
    public void OnEnable()
    {
        _actions.Enable();
    }
    public void OnDisable()
    {
        _actions.Disable();
    }
}
