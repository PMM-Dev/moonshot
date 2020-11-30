using UnityEngine;

public class SmoothTargetFollowing : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _smoothSpeed = 0.125f;
    [SerializeField]
    private Vector3 _offeset;
    public Vector3 Offset
    {
        get
        {
            return _offeset;
        }
        set
        {
            _offeset = value;
        }
    }

    [SerializeField]
    private float _maxCameraMovementOfX;
    [SerializeField]
    private float _minCameraMovementOfX;

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 targetPosition = _target.position + _offeset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, _minCameraMovementOfX, _maxCameraMovementOfX);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed);

        transform.position = smoothedPosition;
    }

    public void SetTarget(GameObject player)
    {
        _target = player.transform;
    }

}