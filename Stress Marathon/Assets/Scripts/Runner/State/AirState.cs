using UnityEngine;

public class AirState : IState
{
    private Runner _runner;
    
    public AirState(Runner runner)
    {
        _runner = runner;
    }
    
    public void Enter()
    {
        _runner.SetAir(true);
    }

    public void Update()
    {
        _runner.SetAirVelocity(_runner.Rb.linearVelocity.y);
            
        if (_runner.IsGrounded())
        {
            if (Mathf.Abs(_runner.Rb.linearVelocity.x) > 0.1)
            {
                _runner.ChangeState(_runner.Move);
            }
            _runner.ChangeState(_runner.Idle);
        }
    }

    public void Exit()
    {
        _runner.SetAir(false);
    }
}
