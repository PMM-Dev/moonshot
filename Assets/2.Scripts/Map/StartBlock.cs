using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class StartBlock : MonoBehaviour
    {
        [SerializeField]
        private GameObject _mapManager;

        private GameObject _Player;
        private bool _isBoard;
        private MapMaking _mapMaking;

        private void Awake()
        {
            _mapMaking = GetComponent<MapMaking>();
        }

        private void Start()
        {
            _Player = MainPlayerManager.Instance.Player;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player") && !_isBoard)
            {
                _mapManager.GetComponent<MapMaking>().WholeMapOrder[0].gameObject.transform.GetChild(0).GetComponent<MakePlayerDie>().CanMakePlayerDie = true;
                _isBoard = true;
            }
        }
    }
}

