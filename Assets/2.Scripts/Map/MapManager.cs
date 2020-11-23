using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        private MapMaking _mapMaking;
        private MapSetActive _mapSetActive;

        //private void Start()
        //{
        //    MainEventManager.Instance.StartMainGameEvent += _mapMaking.CreateStage;
        //    MainEventManager.Instance.StartMainGameEvent += _mapSetActive.makeList;
        //}

        private void Update()//delete later
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _mapMaking.CreateStage();
                _mapSetActive.MakeList();
            }
        }

        private void Awake()
        {
            _mapMaking = GetComponent<MapMaking>();
            _mapSetActive = GetComponent<MapSetActive>();
        }

    }
}

