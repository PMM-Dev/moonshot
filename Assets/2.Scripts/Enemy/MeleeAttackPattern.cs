using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    
    public class MeleeAttackPattern : Pattern
    {

        [SerializeField]
        protected ParticleSystem _patternParticle;
        private void Awake()
        {

            if (_patternParticle != null)
                _patternParticle.Stop();
        }
        override protected void ActuallyPattern()
        {
            if (_playerDistance < _patternRage)
            {
                if (_patternParticle != null)
                {
                    _patternParticle.Play();
                }
                _player.GetComponent<IDamage>().GetDamage();
            }
        }
    }
}