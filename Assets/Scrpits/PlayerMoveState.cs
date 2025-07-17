using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    private PlayerAnimationController _context;
    private Vector2 _currentMoveInput;

    public PlayerMoveState(PlayerAnimationController context)
    {
        _context = context;
    }

    public void EnterState(PlayerAnimationController playerAnimationController)
    {
        _context = playerAnimationController;

        Debug.Log("Entering Idle State");
        _context.SetAnimatorBool("IsMoving", true);
    }

    public void UpdateState()
    {
        _context.SetAnimatorFloat("MoveX", _currentMoveInput.x);
        _context.SetAnimatorFloat("MoveY", _currentMoveInput.y);

        if (_currentMoveInput.magnitude > 0.01f)
        {
            _context.LastMoveDirection = _currentMoveInput.normalized;
        }

        if (_context.LastMoveDirection.x > 0.01f)
        {
            _context.SetSpriteFlipX(false);
        }
        else if (_context.LastMoveDirection.x < -0.01f)
        {
            _context.SetSpriteFlipX(true);
        }
    }

    public void ExitState()
    {
        Debug.Log("Exit");
    }

    public void SetMoveInput(Vector2 input)
    {
        _currentMoveInput = input;
    }
}
