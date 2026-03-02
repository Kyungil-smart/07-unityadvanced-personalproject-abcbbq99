using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class NPCController : Runner
{
    [SerializeField] float _cliffJumpCheakDistance;
    [SerializeField] float _wallCheakDistance;
    [SerializeField] float _wallJumpCheakDistance;
    [SerializeField] float _wallJumpHightCheakDistance;
    [SerializeField] float _backwardTime;
    [SerializeField] private float _stuckThreshold;
    [SerializeField] private float _stuckLimitTime;
    
    private MaterialPropertyBlock _propBlock;
    private SpriteRenderer _renderer;
    private Coroutine _orderCoroutine;
    private bool _isOrder => _orderCoroutine != null? true : false;
    private float _stuckTimer;
    
    private Vector2 _rightUp = new Vector2(1.5f, 5.0f).normalized;
    private Vector2 _leftUp = new Vector2(-1.5f, 5.0f).normalized;
    private Vector2 _rightDown = new Vector2(1,-1).normalized;
    private Vector2 _leftDown = new Vector2(-1,-1).normalized;
    private Vector2 _lastPos;
    
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ChangeMoveSpeedCoroutine());
        SetNPCColor();
        _orderCoroutine = null;
        _stuckTimer = _stuckLimitTime;
    }
    
    protected override void FixedUpdate()
    {
        NPCAutoMovement();
        NPCAutoCliffJump();
        NPCAutoWallJump();
        base.FixedUpdate();
        SaveLastPosition();
    }

    protected override void Update()
    {
        base.Update();
        CountStuckTime();
    }

    void SetNPCColor()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_Color", Random.ColorHSV());
        _renderer.SetPropertyBlock(_propBlock);
    }
    
    void NPCAutoMovement()
    {
        if (!GameManager.IsRacing)
        {
            MoveInput = 0f;
            return;
        }
        
        Ray2D ray = new Ray2D(transform.position, Vector2.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _wallCheakDistance, _groundLayer);
        
        if (hit.collider != null)
        {
            BackwardOrder(_backwardTime);
        }
        else
        {
            if (_isOrder) return;
            MoveInput = 1f;
        }
        
        Debug.Log(MoveInput);
    }
    
    void NPCAutoCliffJump()
    {
        if(!GameManager.IsRacing) return;
        Vector2 pos;
        
        switch (MoveInput)
        {
            case 1f:
                pos = _rightDown;
                break;
            case -1f:
                pos = _leftDown;
                break;
            default:
                pos = Vector2.zero;
                break;
        }
        
        Ray2D ray = new Ray2D(transform.position, pos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _cliffJumpCheakDistance, _groundLayer);
        
        if (hit.collider == null && IsGrounded())
        {
            NPCJump();
        }
    }

    void NPCAutoWallJump()
    {
        if(!GameManager.IsRacing) return;
        
        Vector2 pos;
        Vector2 pos2;
        
        switch (MoveInput)
        {
            case 1f:
                pos = Vector2.right;
                pos2 = _rightUp;
                break;
            case -1f:
                pos = Vector2.left;
                pos2 = _leftUp;
                break;
            default:
                pos = Vector2.zero;
                pos2 = Vector2.zero;
                break;
        }
        
        Ray2D ray = new Ray2D(transform.position, pos);
        Ray2D ray2 = new Ray2D(transform.position, pos2);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _wallJumpCheakDistance, _groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(ray2.origin, ray2.direction, _wallJumpHightCheakDistance, _groundLayer);
        
        if (hit.collider != null && hit2.collider == null && IsGrounded())
        {
            NPCJump();
        }
    }

    void NPCJump(float jumpForce = 13f)
    {
        ChangeJumpForce(jumpForce);
        JumpInput = true;
    }

    void CountStuckTime()
    {
        if(!GameManager.IsRacing) return;
        if(IsHit) return;
        
        float moveDistance = Mathf.Abs(transform.position.x - _lastPos.x);
        
        if(moveDistance <= _stuckThreshold) _stuckTimer -= Time.deltaTime;
        
        if (_stuckTimer <= 0f)
        {
            BackwardOrder(_backwardTime);
            _stuckTimer = _stuckLimitTime;
        }
    }

    void SaveLastPosition()
    {
        _lastPos = transform.position;
    }
    
    public void ChangeJumpForce(float value = 13f)
    {
        _jumpForce = value;
    }
    
    public void BackwardOrder(float backwardTime)
    {
        if (_orderCoroutine != null)
        {
            StopCoroutine(_orderCoroutine);
        }
        _orderCoroutine = StartCoroutine(BackwardCoroutine(backwardTime));
    }
    
    public void StopOrder(float stopTime)
    {
        if (_orderCoroutine != null)
        {
            StopCoroutine(_orderCoroutine);
        }
        _orderCoroutine = StartCoroutine(StopOrderCoroutine(stopTime));
    }

    public void JumpOrder(float jumpForce = 13f)
    {
        NPCJump(jumpForce);
    }

    IEnumerator ChangeMoveSpeedCoroutine()
    {
        _moveSpeed = Random.Range(8f, 11f);
        float delayTime = Random.Range(10f, 30f);
        yield return YieldContainer.WaitForSeconds(delayTime);
        StartCoroutine(ChangeMoveSpeedCoroutine());
    }
    
    IEnumerator BackwardCoroutine(float backwardTime)
    {
        if (_orderCoroutine != null) yield break;
        MoveInput = -1f;
        yield return YieldContainer.WaitForSeconds(backwardTime);
        _orderCoroutine = null;
    }
    
    IEnumerator StopOrderCoroutine(float stopTime)
    {
        if (_orderCoroutine != null) yield break;
        MoveInput = 0f;
        yield return YieldContainer.WaitForSeconds(stopTime);
        _orderCoroutine = null;
    }
}
