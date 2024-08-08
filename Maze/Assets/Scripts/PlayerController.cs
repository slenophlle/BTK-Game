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
        PLInput.Player.Attack.performed += OnAttackPerformed;
    }

    private void Update()
    {
        Vector2 movementVector = GetMovementVectorNormalized();
        playerMovement.HandlePlayerMovement(movementVector);

        bool isWalking = movementVector != Vector2.zero;
        playerMovement.SetWalkingStatus(isWalking);
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            playerMovement.AttackAnimationHandler();
        }
    }

    private Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = PLInput.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
