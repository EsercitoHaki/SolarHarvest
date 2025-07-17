using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    private PlayerAnimationController _context;

    public PlayerIdleState(PlayerAnimationController context)
    {
        _context = context;
    }

    public void EnterState(PlayerAnimationController playerAnimationController)
    {
        _context = playerAnimationController;

        Debug.Log("Entering Idle State");
        _context.SetAnimatorBool("IsMoving", false);
        _context.SetAnimatorFloat("LastMoveX", _context.LastMoveDirection.x);
        _context.SetAnimatorFloat("LastMoveY", _context.LastMoveDirection.y);

        if (_context.LastMoveDirection.x > 0.01f)
        {
            _context.SetSpriteFlipX(false);
        }
        else if (_context.LastMoveDirection.x < -0.01f)
        {
            _context.SetSpriteFlipX(true);
        }
    }

    public void UpdateState()
    {
        
    }
    
    public void ExitState()
    {
        Debug.Log("Exit");
    }

}
