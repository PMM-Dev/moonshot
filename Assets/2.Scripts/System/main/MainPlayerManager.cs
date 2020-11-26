﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class MainPlayerManager : MonoBehaviour
{
    public static MainPlayerManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        Player = GameObject.FindGameObjectWithTag("Player");
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
    private GameObject _playerPrefab;
    [SerializeField]
    private Transform _spawnPos;



    public void SpawnPlayerfromElevator()
    {
        GameObject oldPlayer = Player;
        Player = Instantiate(_playerPrefab, _spawnPos.position, _spawnPos.rotation);
        if (oldPlayer != null) Destroy(oldPlayer);
    }

    public void SetPlayerAsCameraFocus()
    {
        Camera.main.transform.parent.GetComponent<SmoothTargetFollowing>().SetTarget(Player);
    }
}
