using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ExhaustPatterns : Patterns
    {
        private BossLife _bossLife;
        private float _time = 0;

        private void Start()
        {
            _bossLife = this.gameObject.GetComponent<BossLife>();
        }

        public override IEnumerator Run()
        {
            Debug.Log("죽기 가능");
            _bossLife.IsCanAttack = true;
            while (_time < _patternTime) {
                _time += Time.deltaTime;
                
                yield return null;

                if (_bossLife.IsCanAttack == false)
                {
                    break;
                }
                
                }
            Debug.Log("죽기 불가");
            _patternAni.Play("BakcDefult");
            _bossLife.IsCanAttack = false;
            
        }

        public override void Play()
        {
            _patternAni.Play("Exhaust");
        }
    }
}
