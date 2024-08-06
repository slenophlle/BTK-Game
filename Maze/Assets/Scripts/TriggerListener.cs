using UnityEngine;

public class TriggerListener : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.OnObjectTriggered(gameObject);
        }
    }
}
