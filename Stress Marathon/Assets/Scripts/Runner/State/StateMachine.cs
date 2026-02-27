using UnityEngine;

// State를 교체/관리
public class StateMachine
{
    IState _currentState;
    
    // 유니티 씬 상에 존재 하는 '어떤 객체'가 상태(State)를 전환/실행
    public void ChangeState(IState state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState?.Enter();
    }
    
    // 유니티 이벤트 함수인 Update() 랑 다름
    public void Update()
    {
        _currentState?.Update();
    }
}