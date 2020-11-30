using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class MainPlayerManager : MonoBehaviour
{
    public static MainPlayerManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        Player = Instantiate(Resources.Load<GameObject>("Player/Player"));
        Player.SetActive(false);
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

    public void SpawnPlayerfromElevator()
    {
        Player.SetActive(true);
        Player.transform.position = _spawnPos.position;
    }

    public void SetPlayerAsCameraFocus()
    {
        Camera.main.transform.parent.GetComponent<SmoothTargetFollowing>().SetTarget(Player);
    }
}
