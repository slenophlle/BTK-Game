using UnityEngine;

public class TriggerListener : MonoBehaviour
{
    public GameObject Sewer;
    private float moveSpeedZ = 3f;
    private float moveSpeedY = 0.1f;
    private float stopTime = 1f; // Obje durana kadar geçen süre (saniye)
    private float rotationSpeed = 30f; // Rotasyon hýzý (derece/saniye)

    private bool isValid = false;
    private float timer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (!isValid && other.CompareTag("Player"))
        {
            isValid = true;
        }
    }

    private void Update()
    {
        if (isValid)
        {
            timer += Time.deltaTime;

            if (timer < stopTime)
            {
                // Sewer objesini -z ve +y yönünde hareket ettir
                Vector3 movement = new Vector3(0, moveSpeedY * Time.deltaTime, -moveSpeedZ * Time.deltaTime);
                Sewer.transform.Translate(movement);

                // Sewer objesine y ekseninde rotasyon ekle
                Sewer.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
            else
            {
                isValid = false; // Hareketi durdur
            }
        }
    }
}
