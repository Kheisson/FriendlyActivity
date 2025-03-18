using UnityEngine;

public interface IInputListener
{
    void OnMove(Vector2 moveInput);
    void OnJump(bool isJumping);
    void OnSprint(bool isSprinting);
    void OnCamera(bool isCamera);
}