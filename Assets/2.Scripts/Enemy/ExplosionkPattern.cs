using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ExplosionkPattern : Pattern
    {
        override protected void ActuallyPattern()
        {
            if (_WayPointDistance < _patternRage)
            {
                //플레이어 데미지 주는 함수
                Debug.Log("player dead");
            }
            Destroy(this.gameObject);
        }
    }
}