using System.Collections;
using UnityEngine;

public abstract class Runner : MonoBehaviour
{
    public string RunnerName {get; set;}
    
    [SerializeField] protected float _moveSpeed = 10f;
    [SerializeField] protected float _jumpForce = 13f;
    [SerializeField] protected float _hitDrunkTime = 3f;
    
    [SerializeField] protected LayerMask _groundLayer;
    [SerializeField] protected float _groundCheckDistance = 0.75f;
    
    private StateMachine _stateMachine;
    private RaceBoard _raceBoard;
    public IdleState Idle {get; protected set;}
    public MoveState Move {get; protected set;}
    public AirState Air {get; protected set;}
    public HitState Hit {get; protected set;}
    
    public float MoveInput { get; protected set; }
    public bool JumpInput { get; protected set; }
    public bool IsHit;
    
    public Rigidbody2D Rb { get; protected set; }
    protected Animator _animator;
    public Coroutine HitCoroutine;
    
    protected virtual void Awake()
    {
        Init();
    }
    
    protected virtual void Start()
    {
        IsHit = false;
        _stateMachine.ChangeState(Idle);
        AddEntry(this);
    }
    
    protected virtual void Update()
    {
        _stateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        Movement();
        if (JumpInput)
        { 
            JumpInput = false;
            InvokeJump();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }
    
    private void Init()
    {
        _stateMachine = new StateMachine();
        Idle = new IdleState(this);
        Move = new MoveState(this);
        Air = new AirState(this);
        Hit = new HitState(this);
        
        Rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void AddEntry(Runner runner)
    {
        _raceBoard = FindFirstObjectByType<RaceBoard>();
        _raceBoard.AddRunner(runner);
    }
    
    private void Movement()
    {
        if(IsHit) return;
        
        float targetVelocityX = MoveInput * _moveSpeed;
        
        float velocityChange = targetVelocityX - Rb.linearVelocity.x;
        
        Rb.AddForce(new Vector2(velocityChange * Rb.mass, 0), ForceMode2D.Impulse);
        
        if (GameManager.Instance.IsPrized) Rb.linearVelocity = Vector2.zero;
    }

    private void InvokeJump()
    {
        if(IsHit) return;
        if(!IsGrounded()) return;
        
        Rb.AddForce(Vector2.up * _jumpForce,ForceMode2D.Impulse);
    }

    protected void JumpCancel()
    {
        if(Rb.linearVelocity.y <= 0) return;
        Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, Rb.linearVelocity.y * 0.5f);
    }

    public bool IsGrounded()
    {
        Ray2D ray = new Ray2D(transform.position, Vector2.down);
        if(Physics2D.Raycast(ray.origin, ray.direction, _groundCheckDistance, _groundLayer))
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

    public void SetHitRecovery()
    {
        HitCoroutine = StartCoroutine(HitRecoveryCoroutine(_hitDrunkTime));
        HitCoroutine = null;
    }
    
    public IEnumerator HitRecoveryCoroutine(float time)
    {
        yield return YieldContainer.WaitForSeconds(time);
        IsHit = false;
    }
    
    // 애니메이션 설정
    
    public void SetAirVelocity(float velocity)
    {
        _animator.SetFloat("yVelocity", velocity);
    }
    
    public void SetMove(bool value)
    {
        _animator.SetBool("Move", value);
    }
    
    public void SetAir(bool value)
    {
        _animator.SetBool("Air", value);
    }
    
    public void SetHit(bool value)
    {
        _animator.SetBool("Hit", value);
    }
}
