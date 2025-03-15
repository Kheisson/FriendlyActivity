public abstract class PlayerState : IPlayerState
{
    protected readonly PlayerStateMachine _stateMachine;
    protected readonly PlayerAnimationData _animationData;

    public PlayerState(PlayerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _animationData = stateMachine.AnimationData;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }

    protected void SetAnimationBool(string animationBoolName, bool value)
    {
        _stateMachine.Animator.SetBool(animationBoolName, value);
    }

    protected void SetAnimationFloat(string animationFloatName, float value)
    {
        _stateMachine.Animator.SetFloat(animationFloatName, value);
    }

    protected void SetAnimationTrigger(string animationTriggerName)
    {
        _stateMachine.Animator.SetTrigger(animationTriggerName);
    }
}