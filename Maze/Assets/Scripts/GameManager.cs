using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<Transform> objectsToMove; // Hareket ettirilecek objelerin listesi
    public float rotateSpeed = 45f; // Objeleri döndürme hýzý
    public float lifeTime = 2f; // Obje yaþam süresi
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
                // Rotate the object
                obj.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
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
