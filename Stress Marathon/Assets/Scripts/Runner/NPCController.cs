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
    
    private MaterialPropertyBlock _propBlock;
    private SpriteRenderer _renderer;
    private Coroutine _currentCoroutine;
    private bool _isOrder;
    
    private Vector2 _rightUp = new Vector2(1.5f, 5.0f).normalized;
    private Vector2 _leftUp = new Vector2(-1.5f, 5.0f).normalized;
    private Vector2 _rightDown = new Vector2(1,-1).normalized;
    private Vector2 _leftDown = new Vector2(-1,-1).normalized;
    
    protected override void Start()
    {
        _isOrder = false;
        _moveSpeed = Random.Range(8f, 11f);
        SetNPCColor();
        _currentCoroutine = null;
        base.Start();
    }

    protected override void FixedUpdate()
    {
        NPCAutoMovement();
        NPCAutoCliffJump();
        NPCAutoWallJump();
        base.FixedUpdate();
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
        if (_isOrder) return;
        Ray2D ray = new Ray2D(transform.position, Vector2.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _wallCheakDistance, _groundLayer);
        
        if (hit.collider != null)
        {
            _currentCoroutine = StartCoroutine(BackwardCoroutine(_backwardTime));
            _currentCoroutine = null;
        }
        else
        {
            MoveInput = 1f;
        }
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
    
    public void ChangeJumpForce(float value = 13f)
    {
        _jumpForce = value;
    }
    
    public void BackwardOrder(float backwardTime)
    {
        StopCoroutine(_currentCoroutine);
        _currentCoroutine = null;
        StartCoroutine(BackwardCoroutine(backwardTime));
        _currentCoroutine = null;
    }
    
    public void StopOrder(float stopTime)
    {
        StopCoroutine(_currentCoroutine);
        _currentCoroutine = null;
        StartCoroutine(StopOrderCoroutine(stopTime));
        _currentCoroutine = null;
    }

    public void JumpOrder(float jumpForce = 13f)
    {
        NPCJump(jumpForce);
    }
    
    IEnumerator BackwardCoroutine(float backwardTime)
    {
        if (_currentCoroutine != null) yield break;
        _isOrder = true;
        MoveInput = -1f;
        yield return YieldContainer.WaitForSeconds(backwardTime);
        _isOrder = false;
    }
    
    IEnumerator StopOrderCoroutine(float stopTime)
    {
        if (_currentCoroutine != null) yield break;
        _isOrder = true;
        MoveInput = 0f;
        yield return YieldContainer.WaitForSeconds(stopTime);
        _isOrder = false;
    }
}
