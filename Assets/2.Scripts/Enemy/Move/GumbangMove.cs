using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public class GumbangMove : EnemyMove
    {
        override public void FlipSize()
        {
            this.transform.localScale = _reversedScale;
            float rot_z = Mathf.Atan2(_targetDirction.y, _targetDirction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z );
        }
    }
}