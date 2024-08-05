using UnityEngine;

public class TPSCam : MonoBehaviour
{
    public Transform player;  // Reference to the player transform
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the player
    public float rotationSpeed = 5.0f;  // Speed of rotation

    private float currentYaw = 0f; // Current yaw rotation
    private float currentPitch = 0f; // Current pitch rotation

    // Update is called once per frame
    void LateUpdate()
    {
        // Get the mouse input for yaw and pitch
        float yawInput = Input.GetAxis("Mouse X") * rotationSpeed;
        float pitchInput = Input.GetAxis("Mouse Y") * rotationSpeed;

        // Adjust the current yaw and pitch based on input
        currentYaw += yawInput;
        currentPitch -= pitchInput;

        // Clamp the pitch rotation to prevent flipping
        currentPitch = Mathf.Clamp(currentPitch, -20f, 60f);

        // Calculate the desired rotation
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);

        // Update the camera's position based on the player's position and the offset
        transform.position = player.position + rotation * offset;

        // Look at the player
        transform.LookAt(player.position + Vector3.up * 1.5f); // Adjust to look slightly above the player's position
    }
}
