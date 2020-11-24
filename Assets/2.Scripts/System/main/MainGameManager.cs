using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    private static MainGameManager _instance;
    public static MainGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<MainGameManager>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    var newSingleton = new GameObject("Singleton Class").AddComponent<MainGameManager>();
                    _instance = newSingleton;
                }
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<MainGameManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
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
    }

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
        _player = Instantiate(_playerPrefab, _spawnPos.position, _spawnPos.rotation);
        Camera.main.transform.parent.GetComponent<SmoothTargetFollowing>().enabled = true;
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
