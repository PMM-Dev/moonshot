using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField]
    private ElevatorMovement _elevatorMovement;
    [SerializeField]
    private CameraFx _cameraFx;

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Transform _spawnPos;

    private void Start()
    {
        // Pause all game flow before pressing start button
        MainEventManager.Instance.PauseGamePlayEvent?.Invoke();
    }

    public void GameStart()
    {
        StartCoroutine(GameStartCorutine());
    }

    private IEnumerator GameStartCorutine()
    {
        // Set Time.timescale = 1
        MainEventManager.Instance.ResumeGamePlayEvent?.Invoke();

        // Rise elevator
        StartCoroutine(_elevatorMovement.MoveElevator(true));
        _cameraFx.ShakeOfElevatorMovement();

        // Elevator Rising 2f + Delay .8f
        yield return new WaitForSeconds(2.8f);

        // Spawn player
        _player = Instantiate(_player, _spawnPos.position, _spawnPos.rotation);
        Camera.main.transform.parent.GetComponent<SmoothTargetFollowing>().enabled = true;
        Camera.main.transform.parent.GetComponent<SmoothTargetFollowing>().SetTarget(_player);

        // Open elevator door
        yield return StartCoroutine(_elevatorMovement.MoveDoor(true));

        // Start game after door opened
        MainEventManager.Instance.StartMainGameEvent?.Invoke();

        // Close elevator door
        yield return new WaitForSeconds(0.8f);
        yield return StartCoroutine(_elevatorMovement.MoveDoor(false));

        // descend elevator
        StartCoroutine(_elevatorMovement.MoveElevator(false));
        _cameraFx.ShakeOfElevatorMovement();
    }

}
