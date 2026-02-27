using UnityEngine;

public abstract class Runner : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed = 10f;
    [SerializeField] protected float _jumpForce = 13f;
    
    [SerializeField] protected LayerMask _groundLayer;
    [SerializeField] protected float _groundCheckDistance = 0.75f;
    
    private StateMachine _stateMachine;
    public IdleState Idle {get; protected set;}
    public MoveState Move {get; protected set;}
    public AirState Air {get; protected set;}
    
    public float MoveInput { get; protected set; }
    public bool JumpInput { get; protected set; }
    
    public Rigidbody2D Rb { get; protected set; }
    protected Animator _animator;
    
    protected virtual void Awake()
    {
        Init();
    }
    
    private void Start()
    {
        _stateMachine.ChangeState(Idle);
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
    
    private void Init()
    {
        _stateMachine = new StateMachine();
        Idle = new IdleState(this);
        Move = new MoveState(this);
        Air = new AirState(this);
        
        Rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    private void Movement()
    {
        Rb.linearVelocity = new Vector2(MoveInput * _moveSpeed, Rb.linearVelocity.y);
    }

    private void InvokeJump()
    {
        if(!IsGrounded()) return;
        Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, _jumpForce);
    }

    protected void JumpCancel()
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
