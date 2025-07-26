using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoeState : PlayerState
{
    private bool hasHoed = false;
    public PlayerHoeState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0f, 0f);

        triggerCalled = false;
        hasHoed = false;

        PerformHoeAction();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void PerformHoeAction() {
        if (FarmingSystem.Instance == null)
        {
            Debug.LogWarning("FarmingSystem not found!");
            return;
        }

        Vector2 hoeDirection = FarmingSystem.Instance.GetHoeDirection(player.lastMoveX, player.lastMoveY);

        bool success = FarmingSystem.Instance.TryHoeGround(player.transform.position, hoeDirection);

        if (success)
        {
            Debug.Log("Successfully hoed the ground!");
            hasHoed = true;
        }
        else
        {
            Debug.Log("Cannot hoe here!");
        }
    }

    private void PlayHoeEffect()
    {
        //TODO: Spawn particles, dirt effects, etc
    }

    private void PlayerHoeSound()
    {
        //TODO: Player hoe sound effect
    }
}
