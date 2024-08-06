using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControllerSystem PLInput;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        PLInput = new PlayerControllerSystem();
        PLInput.Player.Enable();
        PLInput.Player.Attack.performed += PlayerAttack;
        PLInput.Player.Move.performed += MakeMove;
        PLInput.Player.Move.canceled += StopMove;
    }

    private void MakeMove(InputAction.CallbackContext context)
    {
        Vector2 movementVector = context.ReadValue<Vector2>();
        playerMovement.HandlePlayerMovement(movementVector);

        // Update walking status based on movement input
        bool isWalking = movementVector != Vector2.zero;
        playerMovement.SetWalkingStatus(isWalking);
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        playerMovement.HandlePlayerMovement(Vector2.zero);
        playerMovement.SetWalkingStatus(false);
    }

    private void Update()
    {
        Vector2 movementVector = GetMovementVectorNormalized();
        playerMovement.HandlePlayerMovement(movementVector);

        // Update walking status based on movement input
        bool isWalking = movementVector != Vector2.zero;
        playerMovement.SetWalkingStatus(isWalking);
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            playerMovement.AttackAnimationHandler();
        }
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = PLInput.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized; // Normalize the input vector for consistent movement speed
    }
}
