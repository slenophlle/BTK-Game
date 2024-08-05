using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControllerSystem PLInput;
    private void Awake()
    {
        PLInput = new PlayerControllerSystem();
        PLInput.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = PLInput.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
}
