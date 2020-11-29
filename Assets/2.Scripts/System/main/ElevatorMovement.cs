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

    [SerializeField]
    private Transform _body;

    private SoundHelper _soundHelper;

    private void Awake()
    {
        _soundHelper = gameObject.AddComponent<SoundHelper>();
    }

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
            srcPos = new Vector3(0f, -1.35f, 0f);
            targetPos = new Vector3(0f, 3.4f, 0f);
        }
        else
        {
            srcPos = new Vector3(0f, 3.4f, 0f);
            targetPos = new Vector3(0f, -1.35f, 0f);
        }

        _soundHelper.PlaySound(false, "ElevatorRising");


        float elapsed = 0.0f;
        while (elapsed < _elevatorMovementDuration)
        {
            elapsed += Time.deltaTime / _elevatorMovementDuration;

            _body.transform.localPosition = Vector3.Lerp(srcPos, targetPos, elapsed);


            yield return null;
        }
        _body.transform.localPosition = targetPos;
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
            leftSrcPos = rightSrcPos = new Vector3(-0.5272727f, 0.96f, 0f);
            leftTargetPos = new Vector3(-1.5f - 0.5272727f, 0.96f, 0f);
            rightTargetPos = new Vector3(1.5f - 0.5272727f, 0.96f, 0f);

            // Change render order 
            _leftDoorRenderer.sortingOrder = 15;
            _rightDoorRenderer.sortingOrder = 15;
        }
        else
        {
            leftTargetPos = rightTargetPos = new Vector3(-0.5272727f, 0.96f, 0f);
            leftSrcPos = new Vector3(-1.5f - 0.5272727f, 0.96f, 0f);
            rightSrcPos = new Vector3(1.5f - 0.5272727f, 0.96f, 0f);

            _leftDoorRenderer.sortingOrder = 5;
            _rightDoorRenderer.sortingOrder = 5;
        }

        _soundHelper.PlaySound(false, "ElevatorDoorOpen");

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