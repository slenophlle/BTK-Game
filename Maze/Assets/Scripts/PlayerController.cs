using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAndCameraController : MonoBehaviour
{
    public Rigidbody RigidbodyPL;
    public InputAction PlayerControllersystem;
    public float movementSpeed = 12f;

    public Transform playerCamera;    // Kamera dönüşü
    public float cameraFollowSpeed = 10f;  // Kameranın oyuncuyu takip etme hızı
    public Vector3 cameraOffset;      // Kameranın oyuncudan olan uzaklığı

    public float mouseSensitivity = 100f; // Mouse hassasiyeti
    public float verticalLookLimit = 80f; // Y eksenindeki bakış limitini ayarla

    private float xRotation = 0f;

    Vector2 moveDirection = Vector2.zero;

    void OnEnable()
    {
        PlayerControllersystem.Enable();
    }

    void OnDisable()
    {
        PlayerControllersystem.Disable();
    }

    void Update()
    {
        // Kullanıcıdan hareket yönünü al
        moveDirection = PlayerControllersystem.ReadValue<Vector2>();

        // Mouse hareketi ile kamera dönüşü
        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity * Time.deltaTime;
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void FixedUpdate()
    {
        // X ve Z eksenlerinde hareketi ayarla, Y eksenindeki mevcut hız korunur
        Vector3 velocity = new Vector3(moveDirection.x * movementSpeed, RigidbodyPL.velocity.y, moveDirection.y * movementSpeed);
        RigidbodyPL.velocity = velocity;
    }

    void LateUpdate()
    {
        // Kamerayı oyuncunun etrafında döndür
        if (playerCamera != null)
        {
            Vector3 targetPosition = transform.position + cameraOffset;
            playerCamera.position = Vector3.Lerp(playerCamera.position, targetPosition, cameraFollowSpeed * Time.deltaTime);
            playerCamera.LookAt(transform.position); // Kamerayı oyuncunun baktığı yöne döndürür
        }
    }
}
