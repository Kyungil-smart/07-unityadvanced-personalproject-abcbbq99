using System.Collections;
using UnityEngine;

public class HitState : IState
{
    private Runner _runner;
    
    
    public HitState(Runner runner)
    {
        _runner = runner;
    }
    
    public void Enter()
    {
        _runner.SetAirVelocity(0);
        _runner.SetMoveVelocity(0);
        _runner.SetHit(true);
        _runner.SetHitRecovery();
    }

    public void Update()
    {
        if(_runner.IsHit) return;
        _runner.ChangeState(_runner.Idle);
    }

    public void Exit()
    {
        _runner.SetHit(false);
    }
}
