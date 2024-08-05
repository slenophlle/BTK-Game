using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Transform cameraTransform; // Reference to the camera transform

    [Header("Values")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 10f;
    public float mouseSensitivity = 2f; // Sensitivity for mouse look

    private Rigidbody rb;
    private bool isAttacking = false;
    private bool isWalking = false;
    private float cameraPitch = 0f; // For vertical camera rotation

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    private void Update()
    {
        HandlePlayerMovement();
        HandleCameraRotation();
    }

    public void AttackAnimationHandler()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true); // Set the attack trigger
            Invoke("DisableAttack", 0.5f); // Reset attack after a delay (adjust based on animation length)
        }
    }

    private void DisableAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    private void HandlePlayerMovement()
    {
        // Read input directly for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (!isAttacking) // Disable movement while attacking
        {
            // Calculate camera-relative movement direction
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            cameraForward.y = 0f; // Flatten the forward vector
            cameraRight.y = 0f; // Flatten the right vector

            // Normalize camera direction vectors
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calculate the desired move direction based on camera orientation
            Vector3 desiredMoveDirection = cameraForward * moveDirection.z + cameraRight * moveDirection.x;

            // Apply movement
            transform.position += desiredMoveDirection * moveSpeed * Time.deltaTime;

            // Update walking status
            isWalking = desiredMoveDirection != Vector3.zero;
            animator.SetBool("isWalking", isWalking); // Set walking animation

            // Rotate the player to face the movement direction
            if (isWalking)
            {
                Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    private void HandleCameraRotation()
    {
        // Read mouse movement for camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Adjust the vertical rotation and clamp the angle
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -45f, 45f); // Limit vertical look angle

        // Rotate the camera based on mouse movement
        cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX); // Rotate player around Y-axis for horizontal look
    }
}
