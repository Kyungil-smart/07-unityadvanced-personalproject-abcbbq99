using UnityEngine;

public class AirState : IState
{
    private PlayerController _player;
    private RunnerController _runner;
    
    public AirState(PlayerController player)
    {
        _player = player;
    }
    
    public AirState(RunnerController runner)
    {
        _runner = runner;
    }
    
    public void Enter()
    {
        if (_player != null)
        {
            _player.SetAir(true);
            Debug.Log("AirState");
        }

        if (_runner != null)
        {
            
        }
    }

    public void Update()
    {
        if (_player != null)
        {
            _player.SetAirVelocity(_player.Rb.linearVelocity.y);
            
            if (_player.IsGrounded())
            {
                if (Mathf.Abs(_player.Rb.linearVelocity.x) > 0.1)
                {
                    _player.ChangeState(_player.Move);
                }
                _player.ChangeState(_player.Idle);
            }
        }

        if (_runner != null)
        {
            
        }
    }

    public void Exit()
    {
        if (_player != null)
        {
            _player.SetAir(false);
        }

        if (_runner != null)
        {
            
        }
    }
}
