using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    public Vector2 LastMoveDirection { get; set; } = new Vector2(0, -1);

    private IPlayerState currentState;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);
    }

    private void Start()
    {
        ChangeState(IdleState);
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    public void ChangeState(IPlayerState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;
        currentState.EnterState(this);
    }

    public void SetAnimatorBool(string name, bool value) => Animator.SetBool(name, value);

    public void SetAnimatorFloat(string name, float value) => Animator.SetFloat(name, value);
    public void SetAnimatorTrigger(string name) => Animator.SetTrigger(name);

    public void SetSpriteFlipX(bool flip) => SpriteRenderer.flipX = flip;
}
