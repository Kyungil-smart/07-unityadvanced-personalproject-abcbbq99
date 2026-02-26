using UnityEngine;

public class AirState : IState
{
    private PlayerController _player;
    private EnemyController _enemy;
    
    public AirState(PlayerController player)
    {
        _player = player;
    }
    
    public AirState(EnemyController enemy)
    {
        _enemy = enemy;
    }
    
    public void Enter()
    {
        if (_player != null)
        {
            _player.SetAir(true);
            Debug.Log("AirState");
        }

        if (_enemy != null)
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

        if (_enemy != null)
        {
            
        }
    }

    public void Exit()
    {
        if (_player != null)
        {
            _player.SetAir(false);
        }

        if (_enemy != null)
        {
            
        }
    }
}
