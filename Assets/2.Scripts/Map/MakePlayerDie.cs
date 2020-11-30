using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{ 
    public class MakePlayerDie : MonoBehaviour
    {
        [SerializeField]
        private GameObject _player;
        [SerializeField]
        private bool _canMakePlayerDie;

        private void Start()
        {
            _player = MainPlayerManager.Instance.Player;
        }

        public bool CanMakePlayerDie
        {
            get
            {
                return _canMakePlayerDie;
            }
            set
            {
                _canMakePlayerDie = value;
            }
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_canMakePlayerDie == true && col.gameObject.CompareTag("Player"))
            {
                _player.GetComponent<Player.PlayerController>().GetDamage();
            }
        }
    }
}


