using UnityEngine;

public class MoveState : IState
{
    private Runner _runner;
    
    public MoveState(Runner runner)
    {
        _runner = runner;
    }
    
    public void Enter()
    {
        
    }

    public void Update()
    {
        _runner.SetMoveVelocity(_runner.Rb.linearVelocity.x);
            
        if (!_runner.IsGrounded())
        {
            _runner.ChangeState(_runner.Air);
        }
        else if (Mathf.Abs(_runner.Rb.linearVelocity.x) < 0.1)
        {
            _runner.ChangeState(_runner.Idle);
        }
    }

    public void Exit()
    {
        
    }
}
