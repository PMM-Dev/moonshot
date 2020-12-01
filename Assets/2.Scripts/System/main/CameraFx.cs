using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFx : MonoBehaviour
{
    [SerializeField]
    private float durationOfelevatorMovement;
    [SerializeField]
    private float xMagnitudeOfElevatorMovement;
    [SerializeField]
    private float yMagnitudeOfElevatorMovement;

    private Camera _camera;

    private Coroutine _zoomCoroutine;

    private float _cameraOriginSize;

    [SerializeField]
    private bool _isCameraFXOn;
    public bool IsCameraFXOn
    {
        get { return _isCameraFXOn; }
        set { _isCameraFXOn = value; }
    }

    private void Awake()
    {
        _camera = Camera.main;
        _cameraOriginSize = _camera.orthographicSize;
    }


    public void ShakeOfElevatorMovement()
    {
        if (!_isCameraFXOn)
            return;
        StartCoroutine(Shake(durationOfelevatorMovement, xMagnitudeOfElevatorMovement, yMagnitudeOfElevatorMovement));
    }

    public void CustomShakeOfCamera(float duration, float xMagnitude, float yMagnitude)
    {
        if (!_isCameraFXOn)
            return;
        StartCoroutine(Shake(duration, xMagnitude, yMagnitude));
    }

    private IEnumerator Shake(float duration, float xMagnitude, float yMagnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            // Calculate shake strength for smooth fade
            float halfDuration = duration / 2;
            float halfRate = elapsed < halfDuration ? elapsed : duration - elapsed;
            float fadeValue = halfRate == 0 ? 0 : halfRate / halfDuration; // prevent for NaN

            float x = Random.Range(-1 * fadeValue, fadeValue) * xMagnitude;
            float y = Random.Range(-1 * fadeValue, fadeValue) * yMagnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public void SetZoom(float size, float zoomSpeed, float time)
    {
        if (!_isCameraFXOn)
            return;

        if (_zoomCoroutine != null)
        {
            StopCoroutine(_zoomCoroutine);
            _zoomCoroutine = null;
        }
        StartCoroutine(Zoom(size, zoomSpeed, time));
    }

    public IEnumerator Zoom(float size, float zoomSpeed, float time)
    {
        float progress = 0f;
        float origin = _cameraOriginSize;

        while (progress < 1f)
        {
            progress += Time.deltaTime * zoomSpeed;

            _camera.orthographicSize = Mathf.Lerp(origin, size, progress);

            yield return null;
        }

        yield return new WaitForSeconds(time);

        progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * zoomSpeed;

            _camera.orthographicSize = Mathf.Lerp(size, origin, progress);

            yield return null;
        }
    }
}
