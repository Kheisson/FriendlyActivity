using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Player Settings")] [SerializeField]
    private CharacterController characterController;

    private readonly float movementSpeed = 5f;
    private readonly float jumpForce = 5f;
    private readonly float gravity = -9.81f;

    private Vector2 moveInput;
    private bool usingCamera;
    private bool isJumping;
    private bool isSprinting;
    private float verticalVelocity;
    private readonly List<IInputListener> inputListeners = new();

    private const float GROUNDED_VERTICAL_VELOCITY = -2f;

    private float MovementSpeed => isSprinting ? movementSpeed * 2 : movementSpeed;

    private void Update()
    {
        HandleMovement();
        HandleJumping();
    }

    public void Subscribe(IInputListener listener)
    {
        if (!inputListeners.Contains(listener))
        {
            inputListeners.Add(listener);
        }
    }

    #region Input Event Handlers

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        NotifyListeners(listener => listener.OnMove(moveInput));
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnCamera(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            usingCamera = true;
        }
        else if (context.canceled)
        {
            usingCamera = false;
        }
        
        NotifyListeners(listener => listener.OnCamera(usingCamera));
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && characterController.isGrounded)
        {
            isJumping = true;
            verticalVelocity = jumpForce;
            NotifyListeners(listener => listener.OnJump(isJumping));
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed && characterController.isGrounded && moveInput != Vector2.zero)
        {
            isSprinting = true;
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }

        NotifyListeners(listener => listener.OnSprint(isSprinting));
    }

    #endregion

    #region Player Movement and Look

    private void HandleMovement()
    {
        var moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        moveDirection = transform.TransformDirection(moveDirection) * MovementSpeed;

        characterController?.Move(moveDirection * Time.deltaTime);
    }

    private void HandleJumping()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        else if (verticalVelocity < 0)
        {
            verticalVelocity = GROUNDED_VERTICAL_VELOCITY;
        }

        Vector3 move = new Vector3(0, verticalVelocity, 0);
        characterController?.Move(move * Time.deltaTime);

        if (characterController != null && characterController.isGrounded)
        {
            isJumping = false;
            NotifyListeners(listener => listener.OnJump(isJumping));
        }
    }

    #endregion

    private void NotifyListeners(System.Action<IInputListener> action)
    {
        foreach (var listener in inputListeners)
        {
            action(listener);
        }
    }
}