﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class BossLife : MonoBehaviour
    {
        [SerializeField]
        private int _bossLife = 5;
        private bool _isCanAttack = false;
        public bool IsCanAttack
        {
            set { _isCanAttack = value; }
        }

        public void Attacked() {
            if (_isCanAttack != true)
                return;
            _bossLife--;
            Debug.Log("Life 는" + _bossLife);
            Die();
        }

        public void Die() {
            if (_bossLife > 0)
                return;
            //죽는 애니메이션 출력
            //죽은 뒤에 하는 뭐 클리어창.
        }
    }
}