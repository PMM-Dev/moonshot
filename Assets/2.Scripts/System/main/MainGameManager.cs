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
    private Transform _spawnPos;

    [SerializeField]
    private GameObject _playerPrefab;
    private GameObject _player;
    public GameObject Player
    {
        get
        {
            return _player;
        }
        private set
        {
            _player = value;
        }
    }

    private void Start()
    {
        // Init player property for develop
        Player = GameObject.FindGameObjectWithTag("Player");

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
        Player = Instantiate(_playerPrefab, _spawnPos.position, _spawnPos.rotation);
        Camera.main.transform.parent.GetComponent<SmoothTargetFollowing>().SetTarget(Player);

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
