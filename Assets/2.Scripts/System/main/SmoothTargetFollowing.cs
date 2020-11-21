using UnityEngine;

public class SmoothTargetFollowing : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _smoothSpeed = 0.125f;
    [SerializeField]
    private Vector3 _offeset;

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position + _offeset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }
}