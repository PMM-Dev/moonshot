using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    //
    // SINGLETON

    [SerializeField]
    private ElevatorMovement _elevatorMovement;
    [SerializeField]
    private CameraFx _cameraFx;

    [SerializeField]
    private GameObject _moonPrefab;


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
        MainPlayerManager.Instance.SpawnPlayerfromElevator();

        MainPlayerManager.Instance.SetPlayerAsCameraFocus();

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

    public void SpawnBoss(Transform spawn)
    {
        Instantiate(_moonPrefab, spawn.position, spawn.rotation);
    }
}
