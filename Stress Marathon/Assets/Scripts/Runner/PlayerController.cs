using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Runner
{
    PlayerInputActions _input;

    protected override void Awake()
    {
        base.Awake();
        _input = new PlayerInputActions();
        RunnerName = "Player";
    }
    
    private void OnEnable()
    {
        _input.Enable();

        _input.PlayerActions.Move.performed += OnMove;
        _input.PlayerActions.Move.canceled += OnMove;
        _input.PlayerActions.Jump.performed += OnJump;
        _input.PlayerActions.Jump.canceled += OnJump;
    }

    private void OnDisable()
    {
        _input.PlayerActions.Move.performed -= OnMove;
        _input.PlayerActions.Move.canceled -= OnMove;
        _input.PlayerActions.Jump.performed -= OnJump;
        _input.PlayerActions.Jump.canceled += OnJump;
        
        _input.Disable();
    }
    
    void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        MoveInput = value.x;
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if(ctx.performed) JumpInput = true;
        if(ctx.canceled) JumpCancel();
    }
}
