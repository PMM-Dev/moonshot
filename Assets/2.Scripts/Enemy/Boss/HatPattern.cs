using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class HatPattern : Patterns
    {
        private Hat _hat;
        
        private void Start()
        {
            if (_hat == null)
            {
                _hat = _projectile.GetComponent<Hat>();
            }
            _projectile.gameObject.SetActive(false);
        }

        public override IEnumerator Run()
        {
            _projectile.GetComponent<Hat>().Player = _player;
            _projectile.gameObject.SetActive(true);
            _hat.TargetPlayerPosition();
            _hat.ProjectileSpeed = _projectileSpeed;
            
            while (_projectile.activeSelf == true) {
                yield return null;
            }
            _hat.IsDown = true;
        }
    }
}