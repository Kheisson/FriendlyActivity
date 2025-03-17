using UnityEngine;

public class PlayerStateMachine : MonoBehaviour, IInputListener
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerAnimationData animationData;
    [SerializeField] private InputManager inputManager;

    public Animator Animator { get => animator; } 
    public PlayerAnimationData AnimationData { get => animationData; }

    // Input Values
    public Vector2 MoveInput { get; set; }
    public bool JumpInput { get; set; }
    public bool IsSprinting { get; set; }
    public float VerticalVelocity { get; set; }

    // States
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerSprintState SprintState { get; private set; }

    private PlayerState currentState;

    public bool IsGrounded
    {
        get
        {
            return characterController.isGrounded;
        }
    }

    private void Awake()
    {
        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        SprintState = new PlayerSprintState(this);
    }

    private void Start()
    {
        SetState(IdleState);
        inputManager.Subscribe(this);
    }

    private void Update()
    {
        currentState?.Update();

        animator.SetBool(animationData.BoolGroundedName, IsGrounded);
    }

    public void SetState(PlayerState newState)
    {
        Debug.Log("Transitioning from " + currentState + " to " + newState);
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    #region Input Event Handlers
    public void OnMove(Vector2 moveInput)
    {
        MoveInput = moveInput;
    }

    public void OnJump(bool jumpInput)
    {
        JumpInput = jumpInput;
    }

    public void OnSprint(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        IsSprinting = context.ReadValueAsButton();
    }
    #endregion
}
