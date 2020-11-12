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


    MainDataManager mainEventDataManager;
    MainUIManager mainUIManager;

    public Action StartMainGameEvent;
    public Action PauseGamePlayEvent;

    public void EnemyDeadEvent()
    {
        mainEventDataManager.IncreaseKilledEnemyCount();
    }

    public void GameoverEvent()
    {
        PauseGamePlayEvent.Invoke();
        mainUIManager.ShowGameoverUI(mainEventDataManager.GetKilledEnemyCount().ToString(), mainEventDataManager.GetSurvivedSeconds().ToString());
    }

    private void Start()
    {

    }
}