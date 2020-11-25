using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerManager : MonoBehaviour
{
    public static MainPlayerManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    //
    // SINGLETON

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

    [SerializeField]
    private Transform _spawnPos;

    [SerializeField]
    private GameObject _playerPrefab;


    private void Start()
    {
        // Init player property for develop
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SpawnPlayerfromElevator()
    {
        Player = Instantiate(_playerPrefab, _spawnPos.position, _spawnPos.rotation);
    }

    public void SetPlayerAsCameraFocus()
    {
        Camera.main.transform.parent.GetComponent<SmoothTargetFollowing>().SetTarget(Player);
    }
}
