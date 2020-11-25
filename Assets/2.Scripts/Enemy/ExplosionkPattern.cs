using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ExplosionkPattern : Pattern
    {
        [SerializeField]
        private GameObject _explosionPaticle;
        [SerializeField]
        private float _exPosionkRange = 5f;

        override protected void ActuallyPattern()
        {
            if (_playerDistance < _exPosionkRange)
            {
                _player.GetComponent<IDamage>().GetDamage();
            }
            _explosionPaticle.SetActive(true);
            _explosionPaticle.transform.parent = null;
            _explosionPaticle.transform.position = this.transform.position;
            this.gameObject.SetActive(false);
        }
    }
}