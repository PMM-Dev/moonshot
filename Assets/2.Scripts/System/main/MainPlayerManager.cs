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


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SpawnPlayerfromElevator()
    {
        Player.transform.position = _spawnPos.transform.position;
    }

    public void SetPlayerAsCameraFocus()
    {
        Camera.main.transform.parent.GetComponent<SmoothTargetFollowing>().SetTarget(Player);
    }
}
