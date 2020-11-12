using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System
{
    public class MainEventManager : MonoBehaviour
    {
        MainDataManager mainEventDataManager;

        public Action StartMainGameEvent;
        public Action PauseGamePlayEvent;

        public void KillEnemyEvent()
        {
            mainEventDataManager.IncreaseKilledEnemyCount();
        }

        public void GameoverEvent()
        {
            PauseGamePlayEvent.Invoke();
        }

        private void Start()
        {

        }
    }
}