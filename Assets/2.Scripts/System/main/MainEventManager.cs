using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEventManager : MonoBehaviour
{
    private static MainEventManager _instance;
    public static MainEventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<MainEventManager>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    var newSingleton = new GameObject("Singleton Class").AddComponent<MainEventManager>();
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
        var objs = FindObjectsOfType<MainEventManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    //
    // SINGLETON

    MainDataManager _mainEventDataManager;
    MainUIManager _mainUIManager;


    public Action StartMainGameEvent;
    public Action ResumeGamePlayEvent;
    public Action PauseGamePlayEvent;

    public void EnemyDeadEvent()
    {
        _mainEventDataManager.IncreaseKilledEnemyCount();
    }

    public void GameoverEvent()
    {
        PauseGamePlayEvent?.Invoke();
        Camera.main.transform.parent.GetComponent<SmoothTargetFollowing>().enabled = false;
        _mainUIManager.ShowGameoverUI(_mainEventDataManager.GetKilledEnemyCount().ToString(), _mainEventDataManager.GetSurvivedSeconds().ToString());
    }

    private void Start()
    {
        PauseGamePlayEvent += PauseSystem;
        ResumeGamePlayEvent += ResumSystem;
    }

    private void PauseSystem()
    {
        Time.timeScale = 0;
    }

    private void ResumSystem()
    {
        Time.timeScale = 1f;
    }
}