public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        SetAnimationFloat(_animationData.FloatMoveSpeedName, 0f);
    }

    public override void Update()
    {
        if (_stateMachine.MoveInput != UnityEngine.Vector2.zero)
        {
            _stateMachine.SetState(_stateMachine.MoveState);
            return;
        }

        if (_stateMachine.IsSprinting)
        {
            _stateMachine.SetState(_stateMachine.SprintState);
            return;
        }

        if (_stateMachine.JumpInput)
        {
            _stateMachine.SetState(_stateMachine.JumpState);
        }
    }

    public override void Exit() { }
}