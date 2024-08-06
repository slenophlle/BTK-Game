using UnityEngine;

public class CameraObstacleAvoidance : MonoBehaviour
{
    public Transform target; // The player or the target to follow
    public float defaultDistance = 5f; // Default distance of the camera from the player
    public float minDistance = 1f; // Minimum distance to avoid obstacles
    public float height = 2f; // Default height of the camera from the player
    public float smoothSpeed = 0.125f; // Speed of smoothing
    public LayerMask obstacleLayers; // Layers to check for obstacles

    private Vector3 currentVelocity;

    private void LateUpdate()
    {
        if (target != null)
        {
            HandleCameraMovement();
        }
    }

    private void HandleCameraMovement()
    {
        // Calculate the default position of the camera
        Vector3 desiredPosition = target.position - target.forward * defaultDistance + Vector3.up * height;

        // Check for obstacles using a raycast
        RaycastHit hit;
        Vector3 direction = desiredPosition - (target.position + Vector3.up * height);
        if (Physics.Raycast(target.position + Vector3.up * height, direction.normalized, out hit, defaultDistance, obstacleLayers))
        {
            // If an obstacle is detected, move the camera closer to the player
            float adjustedDistance = Mathf.Clamp(hit.distance, minDistance, defaultDistance);
            desiredPosition = target.position - target.forward * adjustedDistance + Vector3.up * height;
        }

        // Smoothly move the camera to the desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);

        // Make the camera look at the target
        transform.LookAt(target.position + Vector3.up * height);
    }
}
