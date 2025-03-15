public class PlayerSprintState : PlayerState
{
    public PlayerSprintState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        SetAnimationBool(_animationData.BoolIsSprintingName, true);
    }

    public override void Update()
    {
        if (!_stateMachine.IsSprinting)
        {
            if (_stateMachine.MoveInput == UnityEngine.Vector2.zero)
            {
                _stateMachine.SetState(_stateMachine.IdleState);
            }
            else
            {
                _stateMachine.SetState(_stateMachine.MoveState);
            }
            return;
        }

        SetAnimationFloat(_animationData.FloatMoveSpeedName, _stateMachine.MoveInput.magnitude);

        if (_stateMachine.JumpInput)
        {
            _stateMachine.SetState(_stateMachine.JumpState);
        }
    }

    public override void Exit()
    {
        SetAnimationBool(_animationData.BoolIsSprintingName, false);
    }
}