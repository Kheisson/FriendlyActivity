using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float lookSensitivity = 1f;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isFiring;
    private List<IInputListener> inputListeners = new();

    private void Update()
    {
        HandleMovement();
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
        lookInput = context.ReadValue<Vector2>();
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
