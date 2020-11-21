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
                CreateBullet();
        }

        void CreateBullet()
        {
            GameObject bullet = Instantiate(_bulletPrefabs, transform.position, transform.rotation);
            _localScale = bullet.transform.localScale;
            if (_isWolf == true)
            {
                if (this.gameObject.transform.localScale.x < 0)
                    _localScale.x *= -1;
                bullet.transform.localScale = _localScale;
            }
            else
            {
                bullet.GetComponent<Bullet>().IsVertical = true;
                bullet.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            _time = 0;
        }
    }
}