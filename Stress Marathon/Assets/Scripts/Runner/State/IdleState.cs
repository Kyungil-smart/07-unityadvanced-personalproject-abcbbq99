using UnityEngine;

public class IdleState : IState
{
    private Runner _runner;
    
    public IdleState(Runner runner)
    {
        _runner = runner;
    }
    
    public void Enter()
    {
        _runner.SetAirVelocity(0);
        _runner.SetMoveVelocity(0);
    }

    public void Update()
    {
        if (!_runner.IsGrounded())
        {
            _runner.ChangeState(_runner.Air);
        }
        else if (Mathf.Abs(_runner.Rb.linearVelocity.x) > 0.1)
        {
            _runner.ChangeState(_runner.Move);
        }
        
        if(_runner.IsHit) _runner.ChangeState(_runner.Hit);
    }

    public void Exit()
    {
        
    }
}
