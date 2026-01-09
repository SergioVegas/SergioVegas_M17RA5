using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MoveBehavior))]
public  class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private Rigidbody _rb;
    protected MoveBehavior _mb;
    protected float speed = 5;
    private float xVelocity;
    private float zVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mb = GetComponent<MoveBehavior>();
    }

    void FixedUpdate()
    {
        _mb.MoveCharacter(new Vector3(xVelocity, 0,zVelocity), speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        xVelocity = context.ReadValue<Vector3>().x;
        zVelocity = context.ReadValue<Vector3>().z;
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
}
