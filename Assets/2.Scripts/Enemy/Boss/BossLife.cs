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
        private SoundHelper _soundHelper;
        public bool IsCanAttack
        {
            get { return _isCanAttack; }
            set { _isCanAttack = value; }
        }
        public int BossLifes
        {
            get { return _bossLifes; }
        }

        private void Start()
        {
            _soundHelper = this.gameObject.transform.parent.gameObject.GetComponent<SoundHelper>();
        }

        public void Die()
        {
            if (_bossLifes > 0)
                return;
            if (_isDie == true)
                return;
            _isDie = true;
            _soundHelper.PlaySound(false, "Boss_Cry");
            StartCoroutine(DieI());
            Invoke("DieSound", 1f);
            Destroy(this.gameObject, 3f);
            Debug.Log("쥬금");
        }
        public void DieSound() {
            _soundHelper.PlaySound(false, "Boss_Die");
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
            _soundHelper.PlaySound(false, "Boss_Ouch");
            _patternAni.Play("Hit");
            Debug.Log("Life 는" + _bossLifes);
            _isCanAttack = false;
            Die();
            return true;
        }
    }
}