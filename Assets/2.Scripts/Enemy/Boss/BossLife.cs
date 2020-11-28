using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class BossLife : MonoBehaviour, IDamage
    {
        [SerializeField]
        private Animator _patternAni;
        [SerializeField]
        private int _bossLifes = 5;
        private bool _isCanAttack = false;
        private bool _isDie = false;
        public bool IsCanAttack
        {
            get { return _isCanAttack; }
            set { _isCanAttack = value; }
        }
        public int BossLifes
        {
            get { return _bossLifes; }
        }

        public void Die()
        {
            if (_bossLifes > 0)
                return;
            if (_isDie == true)
                return;
            _isDie = true;
            StartCoroutine(DieI());
            _patternAni.Play("Die");
            Destroy(this.gameObject, 3f);
            Debug.Log("쥬금");
            //죽는 애니메이션 출력
            //죽은 뒤에 하는 뭐 클리어창.
        }
        IEnumerator DieI() {
            yield return new WaitForSeconds(2.5f);
            MainEventManager.Instance.ClearMainGameEvent?.Invoke();
        }

        public bool GetDamage()
        {
            Debug.Log("GetDamage");
            if (_isCanAttack != true)
                return false;
            _bossLifes--;
            _patternAni.Play("Hit");
            Debug.Log("Life 는" + _bossLifes);
            _isCanAttack = false;
            Die();
            return true;
        }
    }
}