using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftDoor;
    [SerializeField]
    private GameObject _rightDoor;

    private Transform _leftDoorTransform;
    private Transform _rightDoorTransform;
    private SpriteRenderer _leftDoorRenderer;
    private SpriteRenderer _rightDoorRenderer;

    [SerializeField]
    private float _doorMovementDistance;
    [SerializeField]
    private float _doorMovementDuration;


    [SerializeField]
    private Vector3 _elevatorBottomPosition;
    [SerializeField]
    private Vector3 _elevatorUpPosition;
    [SerializeField]
    private float _elevatorMovementDuration;

    private void Start()
    {
        _leftDoorTransform = _leftDoor.GetComponent<Transform>();
        _leftDoorRenderer = _leftDoor.GetComponent<SpriteRenderer>();
        _rightDoorTransform = _rightDoor.GetComponent<Transform>();
        _rightDoorRenderer = _rightDoor.GetComponent<SpriteRenderer>();
    }



    public IEnumerator MoveElevator(bool isRise)
    {
        Vector3 srcPos;
        Vector3 targetPos;

        if (isRise)
        {
            srcPos = _elevatorBottomPosition;
            targetPos = _elevatorUpPosition;
        }
        else
        {
            srcPos = _elevatorUpPosition;
            targetPos = _elevatorBottomPosition;
        }

        float elapsed = 0.0f;
        while (elapsed < _elevatorMovementDuration)
        {
            elapsed += Time.deltaTime / _elevatorMovementDuration;

            transform.position = Vector3.Lerp(srcPos, targetPos, elapsed);

            yield return null;
        }
        transform.position = targetPos;
    }


    public IEnumerator MoveDoor(bool isOpen)
    {
        Vector3 leftSrcPos;
        Vector3 leftTargetPos;
        Vector3 rightSrcPos;
        Vector3 rightTargetPos;

        if (isOpen)
        {
            // Calculate Source and Target Position
            leftSrcPos = rightSrcPos = Vector3.zero;
            leftTargetPos = new Vector3(-0.7f, 0f, 0f);
            rightTargetPos = new Vector3(0.7f, 0f, 0f);

            // Change render order 
            _leftDoorRenderer.sortingOrder = 10;
            _rightDoorRenderer.sortingOrder = 10;
        }
        else
        {
            leftTargetPos = rightTargetPos = Vector3.zero;
            leftSrcPos = new Vector3(-0.7f, 0f, 0f);
            rightSrcPos = new Vector3(0.7f, 0f, 0f);

            _leftDoorRenderer.sortingOrder = 0;
            _rightDoorRenderer.sortingOrder = 0;
        }

        // Move Animation
        float elapsed = 0.0f;
        while (elapsed < _doorMovementDuration)
        {
            elapsed += Time.deltaTime / _doorMovementDuration;

            _leftDoorTransform.localPosition = Vector3.Lerp(leftSrcPos, leftTargetPos, elapsed);
            _rightDoorTransform.localPosition = Vector3.Lerp(rightSrcPos, rightTargetPos, elapsed);

            yield return null;
        }
        _leftDoorTransform.localPosition = leftTargetPos;
        _rightDoorTransform.localPosition = rightTargetPos;
    }
}