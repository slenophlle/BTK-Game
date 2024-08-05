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