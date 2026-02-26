using UnityEngine;

public class IdleState : IState
{
    private PlayerController _player;
    private EnemyController _enemy;
    
    public IdleState(PlayerController player)
    {
        _player = player;
    }
    
    public IdleState(EnemyController enemy)
    {
        _enemy = enemy;
    }
    
    public void Enter()
    {
        if (_player != null)
        {
            _player.SetAirVelocity(0);
            _player.SetMoveVelocity(0);
            Debug.Log("IdleState");
        }

        if (_enemy != null)
        {
            
        }
    }

    public void Update()
    {
        if (_player != null)
        {
            if (!_player.IsGrounded())
            {
                _player.ChangeState(_player.Air);
            }
            else if (Mathf.Abs(_player.Rb.linearVelocity.x) > 0.1)
            {
                _player.ChangeState(_player.Move);
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
            
        }

        if (_enemy != null)
        {
            
        }
    }
}
