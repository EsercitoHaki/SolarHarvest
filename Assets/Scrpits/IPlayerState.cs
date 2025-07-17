using UnityEngine;

public interface IPlayerState
{
    void EnterState(PlayerAnimationController playerAnimationController);

    void UpdateState();
    void ExitState();
}
