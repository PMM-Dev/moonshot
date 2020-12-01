using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapSetActive : MonoBehaviour
    {
        [SerializeField]
        private GameObject _Player;

        private int _mapIndex = 2;
        private int _recentIndex = 0;
        private List<GameObject> _wholeMapOrder = new List<GameObject>();
        private int _wholeMapOrderCount;
        private MapMaking _mapMaking;

        private void Awake()
        {
            _mapMaking = GetComponent<MapMaking>();
        }
        private void Start()
        {
            _Player = MainPlayerManager.Instance.Player;
        }
        void Update()
        {
            if(_Player != null)
            {
                if (_mapMaking.IsMapCreate == true &&
                    _mapIndex < _wholeMapOrderCount &&
                    _recentIndex < _wholeMapOrderCount &&
                        _Player.transform.position.y >= _wholeMapOrder[_recentIndex].transform.position.y)
                {
                    SetMapActive();
                    SetMapDisActive();
                }
            }
            else
            {
                _Player = MainPlayerManager.Instance.Player;
            }
        }

        public void MakeList()
        {
            int WholeMapOrderCount = gameObject.transform.GetComponent<MapMaking>().WholeMapOrder.Count;
            for (int i = 0; i < WholeMapOrderCount; i++)
                _wholeMapOrder.Add(gameObject.transform.GetComponent<MapMaking>().WholeMapOrder[i]);
            _wholeMapOrderCount = gameObject.transform.GetComponent<MapMaking>().WholeMapOrder.Count;
            _wholeMapOrder[0].SetActive(true);
            _wholeMapOrder[1].SetActive(true);
        }

        void SetMapActive()
        {
            _wholeMapOrder[_mapIndex].SetActive(true);
            _mapIndex+=1;
            _recentIndex+=1;
        }

        void SetMapDisActive()
        {
            if(_recentIndex >= 3)
            {
                if(_recentIndex%2==0)
                {
                    _wholeMapOrder[_recentIndex - 3].gameObject.transform.GetChild(0).GetComponent<MakePlayerDie>().CanMakePlayerDie = true;
                    Transform[] allChildren = _wholeMapOrder[_recentIndex - 3].gameObject.GetComponentsInChildren<Transform>();
                    foreach (Transform child in allChildren)
                    {
                        if (child.name == _wholeMapOrder[_recentIndex - 3].gameObject.name || child.name == "Collider")
                            continue;

                        child.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if(_recentIndex == 3)
                    {
                        _wholeMapOrder[_recentIndex - 3].gameObject.transform.GetChild(0).GetComponent<MakePlayerDie>().CanMakePlayerDie = true;
                        Transform[] allChildren = _wholeMapOrder[_recentIndex - 3].gameObject.GetComponentsInChildren<Transform>();
                        foreach (Transform child in allChildren)
                        {
                            if (child.name == _wholeMapOrder[_recentIndex - 3].gameObject.name || child.name == "Collider" || child.name == "Ground")
                                continue;
                            child.gameObject.SetActive(false);
                        }
                    }
                    else
                        _wholeMapOrder[_recentIndex - 3].SetActive(false);
                }
                    

                if(_recentIndex >=5)
                {
                    _wholeMapOrder[_recentIndex - 5].SetActive(false);
                }
            }
        }
    }
}

