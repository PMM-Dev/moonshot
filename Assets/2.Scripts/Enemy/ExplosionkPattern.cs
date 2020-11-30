using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ExplosionkPattern : Pattern
    {
        [SerializeField]
        private GameObject _explosionPaticle;
        [SerializeField]
        private float _exPosionkRange = 5f;

        override protected void ActuallyPattern()
        {
            if (_playerDistance < _exPosionkRange)
            {
                _player.GetComponent<IDamage>().GetDamage();
            }
            _patternAni.Play("Pattern");
            gameObject.transform.parent.gameObject.AddComponent<SoundHelper>().PlaySound(false,"Boom");
            Instantiate(_explosionPaticle,this.transform.position,this.transform.rotation).gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}