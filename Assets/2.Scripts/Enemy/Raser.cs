using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Raser : MonoBehaviour
    {
        [Tooltip("늑대와 토끼에 맞는 불릿 넣기")] [SerializeField]
        private GameObject _bulletPrefabs;
        [Tooltip("늑대면 체크")][SerializeField]
        private bool _isWolf;
        [SerializeField]
        private float _builletInterver;
        private float _time = 0;
        private Vector3 _localScale;

        private void Update()
        {
            _time += Time.deltaTime;
            if (_time > _builletInterver)
                MakingBullet();
        }

        void MakingBullet()
        {
            Debug.Log("생성");
            GameObject bullet = Instantiate(_bulletPrefabs, transform.position, transform.rotation);
            _localScale = bullet.transform.localScale;
            if (_isWolf == true)
            {
                bullet.GetComponent<Bullet>().IsVertical = false;
                if (this.gameObject.transform.localScale.x < 0)
                    _localScale.x *= -1;
                bullet.transform.localScale = _localScale;

            }
            _time = 0;
        }
    }
}