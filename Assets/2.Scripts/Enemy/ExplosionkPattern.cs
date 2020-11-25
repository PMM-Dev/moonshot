using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ExplosionkPattern : Pattern
    {
        [SerializeField]
        private GameObject _explosionPaticle;

        override protected void ActuallyPattern()
        {
            if (_playerDistance < _patternRage)
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