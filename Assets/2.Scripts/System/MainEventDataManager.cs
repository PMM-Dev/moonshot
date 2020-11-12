using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace System
{
    public class MainDataManager : MonoBehaviour
    {
        private int _killedEnemeyCount;
        private long _survivedSeconds;
        private Stopwatch _sw = new Stopwatch();

        public void IncreaseKilledEnemyCount()
        {
            _killedEnemeyCount++;
        }

        public int GetKilledEnemyCount()
        {
            return _killedEnemeyCount;
        }

        public void StartStopwatch()
        {
            _sw.Reset();
            _sw.Start();
        }

        public void StopStopwatch()
        {
            _sw.Stop();
            _survivedSeconds = _sw.ElapsedMilliseconds / 1000;
        }

        public void RestartStopwatch()
        {
            _sw.Restart();
        }

        public long GetSurvivedSeconds()
        {
            return _survivedSeconds;
        }
    }
}