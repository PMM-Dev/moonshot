using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Hat : Projectile
    {
        private bool _isDown = true;

        private void Update()
        {
            Rotate();
            if (_isDown == true)
            {
                Run();
            }
            else
            {
                ReverseRun();
            }
        }


        protected void ReverseRun()
        {
            this.transform.Translate(Vector3.up * _projectileSpeed * Time.smoothDeltaTime, Space.World);
            if (this.transform.position.y >= _player.transform.position.y + _triggerDistance)
                this.gameObject.SetActive(false);
        }

        override protected void Pattern() {
            _isDown = false;
            SetPosition();
        }

    }
}
