using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Transform cameraTransform;

    [Header("Values")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 0.5f;
    public float mouseSensitivity = 2f;

    private Rigidbody rb;
    private bool isAttacking = false;
    private bool isWalking = false;
    private float cameraPitch = 0f;
    private float cameraYaw = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleCameraRotation();
        UpdateWalkingAnimation();
    }

    public void AttackAnimationHandler()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);
            StartCoroutine(ResetAttack(0.5f));
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
        if (isAttacking) return;

        Vector3 moveDirection = new Vector3(movementVector.x, 0f, movementVector.y).normalized;
        Vector3 desiredMoveDirection = CalculateDesiredMoveDirection(moveDirection);

        MovePlayer(desiredMoveDirection);
        RotatePlayer(desiredMoveDirection);
    }

    private Vector3 CalculateDesiredMoveDirection(Vector3 moveDirection)
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        return cameraForward * moveDirection.z + cameraRight * moveDirection.x;
    }

    private void MovePlayer(Vector3 direction)
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
    public void SetWalkingStatus(bool walking)
    {
        if (isWalking != walking)
        {
            isWalking = walking;
            animator.SetBool("isWalking", walking);
        }
    }
    private void RotatePlayer(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void UpdateWalkingAnimation()
    {
        bool isMoving = transform.forward != Vector3.zero;
        if (isWalking != isMoving)
        {
            isWalking = isMoving;
            animator.SetBool("isWalking", isWalking);
        }
    }

    private void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraPitch = Mathf.Clamp(cameraPitch - mouseY, -45f, 45f);
        cameraYaw += mouseX;

        cameraTransform.eulerAngles = new Vector3(cameraPitch, cameraYaw, 0f);
        cameraTransform.position = transform.position - cameraTransform.forward * 5f + Vector3.up * 2f;

        Vector3 forwardDirection = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;
        transform.forward = forwardDirection;
    }
}
