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
        Debug.Log("IdleState");
    }

    public void Update()
    {
        if (!_runner.IsGrounded())
        {
            _runner.ChangeState(_runner.Air);
        }
        else if (_runner.MoveInput != 0)
        {
            _runner.ChangeState(_runner.Move);
        }
        
        if(_runner.IsHit) _runner.ChangeState(_runner.Hit);
    }

    public void Exit()
    {
        
    }
}
