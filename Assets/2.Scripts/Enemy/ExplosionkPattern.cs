using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ExplosionkPattern : Pattern
    {
        override protected void ActuallyPattern()
        {
            if (_playerDistance < _patternRage)
            {
                _player.GetComponent<IDamage>().GetDamage();
            }
            this.gameObject.SetActive(false);
        }
    }
}