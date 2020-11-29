using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class MadPattern : Patterns
    {
        [SerializeField]
        private float _spwanInterval = 0.5f;
        [SerializeField]
        private List<Meteo> _meteos;

        private void Start()
        {
            for (int i = 0; i < _meteos.Count; i++)
            {
                _meteos[i].ProjectileSpeed = _projectileSpeed;
                _meteos[i].Player = _player;
                _meteos[i].gameObject.SetActive(false);
            }
        }

        public override IEnumerator Run()
        {
            for (int i = 0; i < _meteos.Count; i++) {
                _meteos[i].TargetPlayerPosition();
                _meteos[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(_spwanInterval);
            }
            yield return new WaitForSeconds(_spwanInterval);
            _patternAni.Play("Defult");

        }

        public override void Play()
        {
            _patternAni.Play("Mad");
        }
    }
}