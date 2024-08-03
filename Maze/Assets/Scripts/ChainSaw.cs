using UnityEngine;

public class ChainSaw : MonoBehaviour
{
    // Hareket h�z� ve mesafesi
    public float moveSpeed = 5f;       // Hareket h�z�
    public float moveDistance = 10f;   // Hareket mesafesi
    private float startX;              // Ba�lang�� X konumu
    private bool movingForward = true; // Hareket y�n�

    // Testere b��a��n�n d�n�� h�z�
    public float sawBladeSpeed = 100f; // D�n�� h�z�

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        // X ekseninde ileri geri hareket
        float moveDirection = movingForward ? 1f : -1f;
        Vector3 newPosition = transform.position;
        newPosition.x += moveSpeed * Time.deltaTime * moveDirection;
        transform.position = newPosition;

        if (Mathf.Abs(transform.position.x - startX) >= moveDistance)
        {
            movingForward = !movingForward;
        }

        // Testere b��a��n� d�nd�r
        transform.Rotate(Vector3.forward, sawBladeSpeed * Time.deltaTime);
    }
}
