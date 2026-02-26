using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDistance;
    
    StateMachine _stateMachine;
    public IdleState Idle {get; private set;}
    public MoveState Move {get; private set;}
    public AirState Air {get; private set;}
    
    PlayerInputActions _input;
    public float MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    
    public Rigidbody2D Rb { get; private set; }
    Animator _animator;

    void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.PlayerActions.Move.performed += OnMove;
        _input.PlayerActions.Move.canceled += OnMove;
        _input.PlayerActions.Jump.performed += OnJump;
        _input.PlayerActions.Jump.canceled += OnJump;
    }

    private void OnDisable()
    {
        _input.PlayerActions.Move.performed -= OnMove;
        _input.PlayerActions.Move.canceled -= OnMove;
        _input.PlayerActions.Jump.performed -= OnJump;
        _input.PlayerActions.Jump.canceled += OnJump;
        
        _input.Disable();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        Movement();
        if (JumpInput)
        {
            JumpInput = false;
            InvokeJump();
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * _groundCheckDistance);
    }

    void Init()
    {
        _stateMachine = new StateMachine();
        Idle = new IdleState(this);
        Move = new MoveState(this);
        Air = new AirState(this);
        
        _input = new PlayerInputActions();
        
        Rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _stateMachine.ChangeState(Idle);
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        MoveInput = value.x;
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if(ctx.performed) JumpInput = true;
        if(ctx.canceled) JumpCancel();
    }
    
    void Movement()
    {
        Rb.linearVelocity = new Vector2(MoveInput * _moveSpeed, Rb.linearVelocity.y);
    }

    void InvokeJump()
    {
        if(!IsGrounded()) return;
        Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, _jumpForce);
    }

    void JumpCancel()
    {
        if(Rb.linearVelocity.y <= 0) return;
        Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, Rb.linearVelocity.y * 0.5f);
    }

    public bool IsGrounded()
    {
        Ray2D ray = new Ray2D(transform.position, Vector2.down);
        if(Physics2D.Raycast(ray.origin, ray.direction, _groundCheckDistance, LayerMask.GetMask("Ground")))
        {
            return true;
        }
        return false;
    }
    
    // state 관련
    public void ChangeState(IState state)
    {
        _stateMachine.ChangeState(state);
    }
    
    // 애니메이션 설정
    public void SetMoveVelocity(float velocity)
    {
        _animator.SetFloat("xVelocity", Mathf.Abs(velocity));
    }
    
    public void SetAirVelocity(float velocity)
    {
        _animator.SetFloat("yVelocity", velocity);
    }
    
    public void SetAir(bool value)
    {
        _animator.SetBool("Air", value);
    }
}
