using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerAnimationController playerAnimationController;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
    }

    private void Update()
    {
        Vector2 currentMoveInput = playerInput.MoveInput;

        bool isMoving = currentMoveInput.magnitude > 0.01f;

        if (isMoving)
        {
            playerAnimationController.ChangeState(playerAnimationController.MoveState);
            playerAnimationController.MoveState.SetMoveInput(currentMoveInput);
        }
        else
        {
            playerAnimationController.ChangeState(playerAnimationController.IdleState);
        }

        //if (playerInput.)
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = playerInput.MoveInput.normalized;

        playerMovement.Move(moveDirection);
    }
}