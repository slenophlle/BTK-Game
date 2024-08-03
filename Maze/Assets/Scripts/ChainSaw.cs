using UnityEngine;

public class ChainSaw : MonoBehaviour
{
    // Hareket hýzý ve mesafesi
    public float moveSpeed = 5f;       // Hareket hýzý
    public float moveDistance = 10f;   // Hareket mesafesi
    private float startX;              // Baþlangýç X konumu
    private bool movingForward = true; // Hareket yönü

    // Testere býçaðýnýn dönüþ hýzý
    public float sawBladeSpeed = 100f; // Dönüþ hýzý

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

        // Testere býçaðýný döndür
        transform.Rotate(Vector3.forward, sawBladeSpeed * Time.deltaTime);
    }
}
