using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{ 
    public class MakePlayerDie : MonoBehaviour
    {
        private bool _canMakePlayerDie;
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
                //사망처리
                Debug.Log("주금");
            }
        }
    }
}


