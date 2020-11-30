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
            _time = 0;
            _soundHelper.PlaySound(false, "Boss_Exhausted");
            _bossLife.IsCanAttack = true;

            while (_time < _patternTime)
            {
                _time += Time.deltaTime;


                if (_bossLife.IsCanAttack == false)
                {
                    yield return new WaitForSeconds(1f);
                    if (_bossLife.BossLifes > 0)
                        _patternAni.Play("Defult");
                    break;
                }

                yield return null;
            }

            _bossLife.IsCanAttack = false;
        }

        public override void Play()
        {
            _patternAni.Play("Exhaust");
        }
    }
}
