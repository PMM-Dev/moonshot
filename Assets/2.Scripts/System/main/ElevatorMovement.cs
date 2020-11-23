using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _transform;

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
        StartCoroutine(MoveElevator(_upPosition));
    }

    public void descendElevator()
    {
        StartCoroutine(MoveElevator(_bottomPosition));
    }

    private IEnumerator MoveElevator(Vector3 targetPos)
    {
        float t = 0f;
        while (t <= _movementDuration)
        {
            t += Time.deltaTime;
            _transform.position = Vector3.MoveTowards(_transform.position, targetPos, _movementSpeed * Time.deltaTime);
            yield return null;
        }
        _transform.position = targetPos;
    }
}