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
    protected float speedWalk = 3f;
    protected float speedRunning = 6f;
    protected float actualSpeed;
    private float xVelocity;
    private float zVelocity;
    protected Animator _animator;
    private bool _isAiming = false;
    private bool _wasGrounded;
    private bool _isJumping = false;
    public static event Action UseDoor = delegate { };
    protected EquipWeapon _equipWeapon;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _jb = GetComponent<JumpBehavior>();
        _mb = GetComponent<MoveBehavior>();
        _equipWeapon = GetComponent<EquipWeapon>();
        _actions = new InputSystem_Actions();
        _actions.Player.SetCallbacks(this);
        actualSpeed = speedWalk;
        _wasGrounded = true; 
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

        if (!_wasGrounded && _jb.IsGrounded)
        {

            _animator.SetTrigger("Land");
            _isJumping = false;
        }
        _wasGrounded = _jb.IsGrounded;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        xVelocity = input.x;
        zVelocity = input.y;
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
        if (context.started)
        {
            UseDoor.Invoke();
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (_jb.IsGrounded && !_isJumping)
        {
            _isJumping = true; 
            _animator.SetTrigger("Jump");
            _jb.JumpDelayed();
        }
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
    public void OnDance(InputAction.CallbackContext context)
    {
        _animator.SetTrigger("Dancing");
    }

    public void OnEquip(InputAction.CallbackContext context)
    {
        if (context.started && _equipWeapon != null)
        {
            _equipWeapon.ToggleEquip();
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
