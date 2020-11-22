using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class MeleeAttackPattern : Pattern
    {
        override protected void ActuallyPattern()
        {
            if (_playerDistance < _patternRage)
            {
                _player.GetComponent<IDamage>().GetDamage();
            }
        }
    }
}