using UnityEngine;

public class TriggerListener : MonoBehaviour
{
   // public GameManager gameManager;

    public GameObject objectToMove; // Hareket ettirilecek obje
    public float moveDistance = 5f; // Hareket mesafesi

    private bool hasMoved = false; // Tek seferlik hareket için

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger tetiklendi: " + other.gameObject.name);

        if (!hasMoved)
        {
            if (other.gameObject == objectToMove)
            {
                objectToMove.transform.position += new Vector3(moveDistance, 0, 0);
                hasMoved = true;
            }
        }
    }
}
