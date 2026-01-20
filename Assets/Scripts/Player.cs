using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MoveBehavior))]
[RequireComponent(typeof(JumpBehavior))]

public class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    public event Action<bool> OnAiming;

    protected MoveBehavior _mb;
    protected JumpBehavior _jb;
    private InputSystem_Actions _actions;
    protected float speedWalk = 3;
    protected float speedRunning = 6f;
    protected float actualSpeed;
    private float xVelocity;
    private float zVelocity;
    protected Animator _animator;
    private bool _isAiming = false;

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
        bool isAttacking = _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Player");

        if (isAttacking)
        {
            // Stop movement while attacking
            _mb.ExecuteMovement(Vector3.zero, 0f);
        }
        else
        {
            _animator.SetFloat("Speed", actualSpeed * Mathf.Abs(zVelocity));
            _mb.ExecuteMovement(new Vector3(xVelocity, 0, zVelocity), actualSpeed);
        }

        _animator.SetBool("Grounded", _jb.IsGrounded);
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

    public void OnAim(InputAction.CallbackContext context)
    {
        _isAiming = !_isAiming;
        _animator.SetBool("Aiming", _isAiming);
        OnAiming?.Invoke(_isAiming);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {  
            bool isAttacking = _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Player") ||
                               _animator.GetNextAnimatorStateInfo(0).IsName("Attack_Player");
            if (!isAttacking)
            {
                _animator.SetTrigger("Attack");
            }
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
        if (_jb.IsGrounded)
        {
            _animator.SetTrigger("Jump");
            _jb.JumpDelayed();
        }
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
        if(context.ReadValueAsButton())
        {
            actualSpeed = speedRunning;
        }
        else
        {
            actualSpeed = speedWalk;
        }
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
