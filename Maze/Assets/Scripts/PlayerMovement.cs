using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Transform cameraTransform; // Reference to the camera transform

    [Header("Values")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 15f; // Increased rotation speed
    public float mouseSensitivity = 2f; // Sensitivity for mouse look

    private Rigidbody rb;
    private bool isAttacking = false;
    private bool isWalking = false;
    private float cameraPitch = 0f; // For vertical camera rotation
    private float cameraYaw = 0f; // For horizontal camera rotation

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    private void Update()
    {
        HandleCameraRotation();
    }

    public void AttackAnimationHandler()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true); // Set the attack trigger
            StartCoroutine(ResetAttack(0.5f)); // Adjust based on animation length
        }
    }

    private IEnumerator ResetAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    public void HandlePlayerMovement(Vector2 movementVector)
    {
        // Read input directly for movement
        float horizontal = movementVector.x;
        float vertical = movementVector.y;

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (!isAttacking) // Allow movement while attacking
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

            // Rotate the player to face the movement direction
            if (desiredMoveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            // Update walking animation
            bool isMoving = desiredMoveDirection != Vector3.zero;
            if (isWalking != isMoving)
            {
                isWalking = isMoving;
                animator.SetBool("isWalking", isWalking);
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

        // Adjust the horizontal rotation
        cameraYaw += mouseX;

        // Rotate the camera based on mouse movement
        cameraTransform.eulerAngles = new Vector3(cameraPitch, cameraYaw, 0f);
        cameraTransform.position = transform.position - cameraTransform.forward * 5f + Vector3.up * 2f; // Adjust the camera distance and height
    }

    public void SetWalkingStatus(bool walking)
    {
        if (isWalking != walking)
        {
            isWalking = walking;
            animator.SetBool("isWalking", walking);
        }
    }
}
