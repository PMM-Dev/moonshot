using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEventManager : MonoBehaviour
{
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
        mainUIManager.WriteGameoverUI(mainEventDataManager.GetKilledEnemyCount().ToString(), mainEventDataManager.GetSurvivedSeconds().ToString());
    }

    private void Start()
    {

    }
}