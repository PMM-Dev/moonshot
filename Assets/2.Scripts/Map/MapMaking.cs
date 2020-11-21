using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapMaking : MonoBehaviour
    {
        [SerializeField]
        private int _stagePerMapCount;//need map per stage
        [SerializeField]
        private GameObject _mapCandidate;
        [SerializeField]
        private float _mapYlength;
        [SerializeField]
        private float _startPointY;

        private int[] _mapFrequency = new int[20];//not two times more selected when first random check
        private int[] _mapFrequencyCheck = new int[20];// check the frequency when locating
        private int _mapCount;//count candidate map*2 including duplicate 
        List<int> _mapOrder = new List<int>();//random order map

        private void Start()
        {
            MainEventManager.Instance.StartMainGameEvent += CreateMap;
        }

        void CreateMap() 
        {
            _mapCount = _mapCandidate.transform.childCount;

            while (true)
            {
                if (_mapOrder.Count == _stagePerMapCount)
                    break;

                int ran = Random.Range(0, _mapCount / 2);

                if (_mapFrequency[ran] >= 2)
                    continue;

                _mapOrder.Add(ran);
                _mapFrequency[ran]++;
            }

            //Code it after make map prefab
            for (int i = 0; i < _stagePerMapCount; i++)
            {
                int idx = _mapOrder[i];
                Vector3 targetPosition = new Vector3(0, _startPointY + _mapYlength * (i + 1), 0);
                if (_mapFrequencyCheck[idx] == 0)
                {
                    GameObject game = _mapCandidate.transform.GetChild(idx).gameObject;
                    game.transform.position = targetPosition;
                    _mapFrequencyCheck[idx]++;
                }
                else if (_mapFrequencyCheck[idx] == 1)
                {
                    GameObject game = _mapCandidate.transform.GetChild(idx + _mapCount / 2).gameObject;
                    game.transform.position = targetPosition;
                    _mapFrequencyCheck[idx]++;
                }
                else
                    continue;
            }
        }
    }

}
