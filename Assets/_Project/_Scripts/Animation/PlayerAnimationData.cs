using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAnimationData", menuName = "Animation/PlayerAnimationData")]
public class PlayerAnimationData : ScriptableObject
{
    [Header("General")]
    [SerializeField] private string boolGroundedName = "Grounded";
    [SerializeField] private  string floatMoveSpeedName = "MoveSpeed";
    [SerializeField] private  string floatVerticalVelocityName = "VerticalVelocity";

    [Header("Jump")]
    [SerializeField] private  string triggerJumpName = "Jump";

    [Header("Sprint")]
    [SerializeField] private  string boolIsSprintingName = "IsSprinting";
    
    public string BoolGroundedName => boolGroundedName;
    public string FloatMoveSpeedName => floatMoveSpeedName;
    public string FloatVerticalVelocityName => floatVerticalVelocityName;
    public string TriggerJumpName => triggerJumpName;
    public string BoolIsSprintingName => boolIsSprintingName;
    
}