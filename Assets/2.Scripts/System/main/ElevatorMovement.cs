using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 _bottomPosition;
    [SerializeField]
    private Vector3 _upPosition;
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _movementDuration;


    private void Start()
    {
        RiseElevator();
    }

    public void RiseElevator()
    {
        StartCoroutine(MoveElevator(_bottomPosition, _upPosition));
    }

    public void descendElevator()
    {
        StartCoroutine(MoveElevator(_upPosition, _bottomPosition));
    }

    private IEnumerator MoveElevator(Vector3 srcPos, Vector3 targetPos)
    {
        float elapsed = 0.0f;
        while (elapsed < _movementDuration)
        {
            elapsed += Time.deltaTime / _movementDuration;

            transform.position = Vector3.Lerp(srcPos, targetPos, elapsed);

            yield return null;
        }
        transform.position = targetPos;
    }
}