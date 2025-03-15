public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        SetAnimationBool("IsMoving", true);
    }

    public override void Update()
    {
        SetAnimationFloat(_animationData.FloatMoveSpeedName, _stateMachine.MoveInput.x);
        SetAnimationFloat("VerticalMoveSpeed", _stateMachine.MoveInput.y);
        
        if (_stateMachine.MoveInput == UnityEngine.Vector2.zero)
        {
            _stateMachine.SetState(_stateMachine.IdleState);
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

    public override void Exit()
    {
        SetAnimationBool("IsMoving", false);
    }
}