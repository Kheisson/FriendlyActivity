using UnityEngine;

namespace _Project._Scripts.StateMachines
{
    public class CameraStateMachine : MonoBehaviour, IInputListener
    {
        [SerializeField] private Animator animator;
        [SerializeField] private InputManager inputManager;

        private bool isThirdPerson = true;
        
        private readonly int ThirdPersonCameraHash = Animator.StringToHash("TPC");
        private readonly int FirstPersonCameraHash = Animator.StringToHash("FPC");
        
        private void Start()
        {
            inputManager.Subscribe(this);
        }

        public void OnCamera(bool isCamera)
        {
            if (isCamera)
            {
                isThirdPerson = !isThirdPerson;
                animator.Play(isThirdPerson ? ThirdPersonCameraHash : FirstPersonCameraHash);
            }
        }

        public void OnMove(Vector2 moveInput) { }

        public void OnJump(bool isJumping) { }

        public void OnSprint(bool isSprinting) { }
    }
}