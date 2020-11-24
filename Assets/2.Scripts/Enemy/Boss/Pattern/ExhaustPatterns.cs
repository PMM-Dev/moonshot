using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ExhaustPatterns : Patterns
    {
        [SerializeField][Range(1,10)]
        private float _grogyTime = 1;
        private BossLife _bossLife;
        private float _time = 0;

        private void Start()
        {
            _bossLife = this.gameObject.GetComponent<BossLife>();
        }

        public override IEnumerator Run()
        {
            Debug.Log("죽기 가능");
            //지친 애니 출력
            _bossLife.IsCanAttack = true;
            while (_time < _grogyTime) {
                _time += Time.deltaTime;
                
                yield return null;

                if (_bossLife.IsCanAttack == false)
                    break;
            }
            Debug.Log("죽기 불가");
            _bossLife.IsCanAttack = false;
            
        }

    }
}
