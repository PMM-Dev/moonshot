using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class MaspeopleMove : EnemyMove
    {

        override protected IEnumerator TrackingPlayer()
        {

            _time = 0;
            _startPosition = this.transform.position;

            while (true)
            {
                _speed += 0.1f;
                _speed = Mathf.Clamp(_speed, 1f, _maxSpeed);
                transform.Translate(_playerDirction * _speed * Time.smoothDeltaTime, Space.World);
                FilpPlayer();
                yield return null;
                PlayerDistanceCalculation();
                if (this.gameObject.activeSelf == false)
                    break;
            }
        }

        void FilpPlayer() {
            if (_playerDirction.x < 0)
                this.transform.localScale = _originScale;
            else
                this.transform.localScale = _reversedScale;


        }
    }

}