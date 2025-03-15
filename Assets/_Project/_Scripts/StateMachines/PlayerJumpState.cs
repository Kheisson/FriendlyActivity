using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private float startJumpTime;
    private readonly float jumpForce = 10f; 

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        startJumpTime = Time.time; 
        SetAnimationTrigger(_animationData.TriggerJumpName);
        _stateMachine.VerticalVelocity = jumpForce;
    }

    public override void Update()
    {
        SetAnimationFloat(_animationData.FloatVerticalVelocityName, _stateMachine.VerticalVelocity);

        if (_stateMachine.IsGrounded)
        {
            if (_stateMachine.MoveInput == Vector2.zero)
            {
                _stateMachine.SetState(_stateMachine.IdleState);
            }
            else
            {
                _stateMachine.SetState(_stateMachine.MoveState);
            }
        }
    }

    public override void Exit() { }
}