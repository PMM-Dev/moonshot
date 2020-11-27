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
            Debug.Log("공격");
            if (_patternParticle != null)
                _patternParticle.Play();
            if (_playerDistance < _patternRage)
                _player.GetComponent<IDamage>().GetDamage();
        }


        override protected void Animation()
        {
            Debug.Log("애니 시작");
            if (_patternAni != null)
                _patternAni.Play("WolfMeelAttack");
            if (_soundhelper != null)
                _soundhelper.PlaySound(false, "WolfAttack");
        }
    }
}