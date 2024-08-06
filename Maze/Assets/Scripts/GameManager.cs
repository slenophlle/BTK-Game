using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<Transform> objectsToMove; // Hareket ettirilecek objelerin listesi
    public float moveSpeed = 5f; // Objeleri hareket ettirme h�z�
    public float lifeTime = 2f; // Obje ya�am s�resi
    private Dictionary<Transform, bool> moveStatus = new Dictionary<Transform, bool>();

    private void Start()
    {
        foreach (Transform obj in objectsToMove)
        {
            moveStatus[obj] = false;
        }
    }

    private void Update()
    {
        foreach (Transform obj in objectsToMove)
        {
            if (moveStatus[obj])
            {
                obj.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        }
    }

    public void OnObjectTriggered(GameObject triggeredObject)
    {
        Transform objTransform = triggeredObject.transform;

        if (moveStatus.ContainsKey(objTransform))
        {
            moveStatus[objTransform] = true;
            Destroy(triggeredObject, lifeTime);
        }
    }
}
