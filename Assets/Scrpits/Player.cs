using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed;

    #region Compoments
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerHoeState hoeState { get; private set; }
    #endregion

    public float lastMoveX { get; private set; }
    public float lastMoveY { get; private set; }

    public int facingDir { get; private set; } = 1;

    protected virtual void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        hoeState = new PlayerHoeState(this, stateMachine, "Hoe");

        lastMoveX = 0f;
        lastMoveY = -1f;
    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        stateMachine.Initialize(idleState);
    }

    protected virtual void Update()
    {
        stateMachine.currentState.Update();
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);

        if (_xVelocity != 0 || _yVelocity != 0)
        {
            lastMoveX = _xVelocity;
            lastMoveY = _yVelocity;
        }
    }

    public virtual void UpdateAnimationParameters()
    {
        float moveX = rb.velocity.x;
        float moveY = rb.velocity.y;

        CheckForFlip(moveX);

        anim.SetFloat("MoveX", Mathf.Abs(moveX));
        anim.SetFloat("MoveY", moveY);
        anim.SetFloat("LastMoveX", Mathf.Abs(lastMoveX));
        anim.SetFloat("LastMoveY", lastMoveY);
    }

    private void CheckForFlip(float _xVelocity)
    {
        if (_xVelocity > 0 && facingDir == -1)
        {
            Flip();
        }
        else if (_xVelocity < 0 && facingDir == 1)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        sr.flipX = !sr.flipX;
    }
    
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
