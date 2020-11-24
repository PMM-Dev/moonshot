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

        private void Start()
        {
            _bossLife = this.gameObject.GetComponent<BossLife>();
        }

        public override IEnumerator Run()
        {
            //지친 애니 출력
            _bossLife.IsCanAttack = true;
            yield return new WaitForSeconds(_grogyTime);
            //회복 애니 출력
            _bossLife.IsCanAttack = false;
            
        }
    }
}
