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
    private bool _wasGrounded;
    private bool _isJumping = false;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _jb = GetComponent<JumpBehavior>();
        _mb = GetComponent<MoveBehavior>();
        _actions = new InputSystem_Actions();
        _actions.Player.SetCallbacks(this);
        actualSpeed = speedWalk;
        _wasGrounded = true; // Asumimos que el personaje empieza en el suelo
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

        // Este booleano sigue siendo útil para pasar de Saltar a Caer (Jumping -> Falling)
        _animator.SetBool("Grounded", _jb.IsGrounded);

        // --- LÓGICA DE ATERRIZAJE ---
        // Comprobamos si acabamos de aterrizar en este fotograma
        if (!_wasGrounded && _jb.IsGrounded)
        {
            // Usamos un Trigger para la animación de aterrizaje, que solo se dispara una vez
            _animator.SetTrigger("Land");
            // Al aterrizar, reseteamos la variable para permitir un nuevo salto
            _isJumping = false;
        }

        // Almacenamos el estado de 'Grounded' para usarlo en el siguiente fotograma
        _wasGrounded = _jb.IsGrounded;
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
        // Solo saltar si estamos en el suelo Y NO estamos ya en proceso de saltar
        if (_jb.IsGrounded && !_isJumping)
        {
            _isJumping = true; // Marcamos que hemos empezado un salto
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
