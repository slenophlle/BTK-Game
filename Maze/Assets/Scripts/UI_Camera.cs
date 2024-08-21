using UnityEngine;
using DG.Tweening;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float Duration;

    public void LookAt(Transform target)
    {
        transform.DOLookAt(target.position, Duration);
    }

}
