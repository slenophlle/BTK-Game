using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 5f;   // Movement speed of the character
    public float rotationSpeed = 720f; // Rotation speed of the character

    [Header("Camera Settings")]
    public CinemachineFreeLook freeLookCamera; // Reference to Cinemachine FreeLook camera

    private CharacterController characterController;
    private Vector2 inputDirection;
    private Transform cameraTransform;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        // Enable the input actions
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Move"].performed += OnMovePerformed;
        playerInput.actions["Move"].canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        // Disable the input actions
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Move"].performed -= OnMovePerformed;
        playerInput.actions["Move"].canceled -= OnMoveCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Read movement input
        inputDirection = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Reset movement input when canceled
        inputDirection = Vector2.zero;
    }

    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        // Convert input direction to camera-relative direction
        Vector3 moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y).normalized;
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f; // Keep only horizontal direction
        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0f;

        // Calculate movement direction based on camera orientation
        Vector3 desiredDirection = cameraForward * moveDirection.z + cameraRight * moveDirection.x;

        // Move the character
        if (moveDirection.magnitude >= 0.1f)
        {
            // Smoothly rotate the character to face the desired direction
            Quaternion targetRotation = Quaternion.LookRotation(desiredDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Apply movement
            Vector3 movement = desiredDirection * movementSpeed * Time.deltaTime;
            characterController.Move(movement);
        }
    }
}
