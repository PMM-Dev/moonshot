using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEventManager : MonoBehaviour
{
    public static MainEventManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
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