using UnityEngine;

public class SmoothTargetFollowing : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _smoothSpeed = 0.125f;
    [SerializeField]
    private Vector3 _offeset;

    [SerializeField]
    private float _maxCameraMovementOfX;
    [SerializeField]
    private float _minCameraMovementOfX;

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position + _offeset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, _maxCameraMovementOfX, _minCameraMovementOfX);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed);

        transform.position = smoothedPosition;
    }
}