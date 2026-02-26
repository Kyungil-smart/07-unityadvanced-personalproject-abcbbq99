using UnityEngine;

public class MoveState : IState
{
    private PlayerController _player;
    private EnemyController _enemy;
    
    public MoveState(PlayerController player)
    {
        _player = player;
    }
    
    public MoveState(EnemyController enemy)
    {
        _enemy = enemy;
    }
    
    public void Enter()
    {
        if (_player != null)
        {
            Debug.Log("MoveState");
        }

        if (_enemy != null)
        {
            
        }
    }

    public void Update()
    {
        if (_player != null)
        {
            _player.SetMoveVelocity(_player.Rb.linearVelocity.x);
            
            if (!_player.IsGrounded())
            {
                _player.ChangeState(_player.Air);
            }
            else if (Mathf.Abs(_player.Rb.linearVelocity.x) < 0.1)
            {
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
            
        }

        if (_enemy != null)
        {
            
        }
    }
}
