using UnityEngine;

public class TPSCam : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // The player or target the camera follows

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 5f, -7f); // Camera offset from the player
    public float followSpeed = 10f; // Speed at which the camera follows the player
    public float rotationSpeed = 5f; // Speed at which the camera rotates to face the player
    public float mouseSensitivity = 2f; // Sensitivity for mouse look
    public float minYAngle = -45f; // Minimum vertical angle
    public float maxYAngle = 45f; // Maximum vertical angle

    private float currentYaw = 0f; // Current horizontal angle
    private float currentPitch = 0f; // Current vertical angle

    private void LateUpdate()
    {
        // Ensure the target is not null
        if (target == null)
        {
            Debug.LogWarning("TPSCam: Target is not set.");
            return;
        }

        HandleCameraRotation();

        // Calculate the desired position and smoothly move the camera to that position
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Rotate the camera to face the target smoothly
        Quaternion desiredRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleCameraRotation()
    {
        // Read mouse movement for camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Adjust the yaw (horizontal rotation) and pitch (vertical rotation)
        currentYaw += mouseX;
        currentPitch -= mouseY;

        // Clamp the pitch angle
        currentPitch = Mathf.Clamp(currentPitch, minYAngle, maxYAngle);
    }
}
