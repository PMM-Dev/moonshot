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
                //플레이어 데미지 주는 함수
                Debug.Log("player dead");
            }
        }
    }
}