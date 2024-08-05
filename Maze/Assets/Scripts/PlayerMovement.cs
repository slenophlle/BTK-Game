using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour

{   public PlayerController playerController;
    private Rigidbody rb;


    public float moveSpeed = 3f;

    private bool iswalking;
    
    private void Update()
    {
        HandlePlayerMovement();
    }
    private void HandlePlayerMovement()
    {
        Vector2 inputVector = playerController.GetMovementVectorNormalized();



        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        iswalking = moveDirection != Vector3.zero;

        float rotationSpeed = 10f;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

    }
}
