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
        void Update()
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

        public void MakeList()
        {
            int WholeMapOrderCount = gameObject.transform.GetComponent<MapMaking>().WholeMapOrder.Count;
            for (int i = 0; i < WholeMapOrderCount; i++)
                _wholeMapOrder.Add(gameObject.transform.GetComponent<MapMaking>().WholeMapOrder[i]);
            _wholeMapOrderCount = gameObject.transform.GetComponent<MapMaking>().WholeMapOrder.Count;
            _wholeMapOrder[0].SetActive(true);
            _wholeMapOrder[1].SetActive(true);
            _wholeMapOrder[1].gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        void SetMapActive()
        {
            _wholeMapOrder[_mapIndex].SetActive(true);
            if(_mapIndex%2 == 1)
            {
                _wholeMapOrder[_mapIndex].gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            _mapIndex+=1;
            _recentIndex+=1;
        }

        void SetMapDisActive()
        {
            if(_recentIndex >= 3)
            {
                if(_recentIndex%2==0)
                {
                    _wholeMapOrder[_recentIndex - 3].gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    _wholeMapOrder[_recentIndex - 3].gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    _wholeMapOrder[_recentIndex - 3].gameObject.GetComponent<MakePlayerDie>().CanMakePlayerDie = true;
                    Transform[] allChildren = _wholeMapOrder[_recentIndex - 3].gameObject.GetComponentsInChildren<Transform>();
                    foreach (Transform child in allChildren)
                    {
                        if (child.name == _wholeMapOrder[_recentIndex - 3].gameObject.name)
                            continue;

                        child.gameObject.SetActive(false);
                    }
                }
                else
                    _wholeMapOrder[_recentIndex - 3].SetActive(false);

                if(_recentIndex >=5)
                {
                    _wholeMapOrder[_recentIndex - 5].SetActive(false);
                }
            }
        }
    }
}

