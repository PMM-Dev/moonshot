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


    public void ShakeOfElevatorMovement()
    {
        StartCoroutine(Shake(durationOfelevatorMovement, xMagnitudeOfElevatorMovement, yMagnitudeOfElevatorMovement));
    }

    public void CustomShakeOfCamera(float duration, float xMagnitude, float yMagnitude)
    {
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
}
