using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Player Settings")] [SerializeField]
    private CharacterController characterController;

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;

    private Vector2 moveInput;
    private bool isFiring;
    private bool isJumping;
    private float verticalVelocity;
    private readonly List<IInputListener> inputListeners = new();

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

        foreach (var listener in inputListeners)
        {
            listener.OnMove(moveInput);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isFiring = true;
            Fire();
        }
        else if (context.canceled)
        {
            isFiring = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && characterController.isGrounded)
        {
            isJumping = true;
            verticalVelocity = jumpForce;

            foreach (var listener in inputListeners)
            {
                listener.OnJump(isJumping);
            }
        }
    }

    #endregion

    #region Player Movement and Look

    private void HandleMovement()
    {
        var moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= movementSpeed;

        if (characterController != null)
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    private void HandleJumping()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        else if (verticalVelocity < 0)
        {
            verticalVelocity = -2f; 
        }

        Vector3 move = new Vector3(0, verticalVelocity, 0);
        characterController.Move(move * Time.deltaTime);

        if (characterController.isGrounded)
        {
            isJumping = false;
            
            foreach (var listener in inputListeners)
            {
                listener.OnJump(isJumping);
            }
        }
    }

    #endregion

    #region Firing

    private void Fire()
    {
        if (isFiring)
        {
            Debug.Log("Fire action triggered!");
        }
    }

    #endregion
}