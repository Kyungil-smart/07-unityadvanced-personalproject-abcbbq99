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
        Debug.Log("MoveState");
        _runner.SetMove(true);
    }

    public void Update()
    {
        if (!_runner.IsGrounded())
        {
            _runner.ChangeState(_runner.Air);
        }
        else if (Mathf.Abs(_runner.Rb.linearVelocity.x) < 0.1 && _runner.MoveInput == 0)
        {
            _runner.ChangeState(_runner.Idle);
        }
        
        if(_runner.IsHit) _runner.ChangeState(_runner.Hit);
    }

    public void Exit()
    {
        _runner.SetMove(true);
    }
}
