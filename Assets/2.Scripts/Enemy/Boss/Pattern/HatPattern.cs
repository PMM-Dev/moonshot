using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class HatPattern : Patterns
    {
        private Hat _hat;
        
        private void Awake()
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
            _hat.TargetPlayerPosition();
            _hat.ProjectileSpeed = _projectileSpeed;
            
            _projectile.gameObject.SetActive(true);
            _hat.StartCoroutine(_hat.Down());
            while (_projectile.activeSelf == true) {
                yield return null;
            }
        }

        public override void Play()
        {
            _patternAni.Play("Hat");
        }
    }
}