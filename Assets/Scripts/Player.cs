using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MoveBehavior))]
[RequireComponent(typeof(JumpBehavior))]

public class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    protected MoveBehavior _mb;
    protected JumpBehavior _jb;
    private InputSystem_Actions _actions;
    protected float speedWalk = 3;
    protected float speedRunning = 6f;
    protected float actualSpeed;
    private float xVelocity;
    private float zVelocity;
    protected Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _jb = GetComponent<JumpBehavior>();
        _mb = GetComponent<MoveBehavior>();
        _actions = new InputSystem_Actions();
        _actions.Player.SetCallbacks(this);
        actualSpeed = speedWalk;
    }

    void Update()
    {   
        _animator.SetFloat("Speed", Mathf.Abs(actualSpeed)* zVelocity);
        _mb.ExecuteMovement(new Vector3(xVelocity, 0,zVelocity), actualSpeed);
        if (_jb.IsGrounded)
        {
            _animator.SetBool("Grounded", true);
        }
        else
            _animator.SetBool("Grounded", false);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>(); 
        xVelocity = input.x;
        zVelocity = input.y;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new 
            
            ();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        _animator.SetTrigger("Dancing");
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _jb.Jump();
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
        actualSpeed = speedRunning;
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
