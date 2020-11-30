using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float _bulletSpeed;
        [SerializeField]
        private float _distroyTime = 3f;
        [SerializeField]
        private float _distroyYlocation = 3f;
        [SerializeField]
        private bool _isVertical;
        private float _playerCorrectionValue = 100 / 8;
        private Vector3 _playerDirction;
        private GameObject _player;
        public bool IsVertical
        {
            set
            {
                _isVertical = value;
            }
        }
        private void Start()
        {
            _player = MainPlayerManager.Instance.Player;
            PlayerDirctionCalculation();
            LookBullet();
            Destroy(this.gameObject, _distroyTime);
        }


        private void Update()
        {
            if (_isVertical == true && _playerDirction.y>0)
            {
                Destroy(this);
                return;
            }
            transform.Translate(_playerDirction * _bulletSpeed * Time.smoothDeltaTime, Space.World);
        }

        void LookBullet() {
            float _angle = Mathf.Atan2(_playerDirction.y, _playerDirction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
        }

        void PlayerDirctionCalculation()
        {
            Vector3 _tmpPlayerPosition;
            _tmpPlayerPosition = _player.transform.position;
            _tmpPlayerPosition.y += _player.transform.localScale.y * _playerCorrectionValue;
            _playerDirction = (_tmpPlayerPosition - this.gameObject.transform.position).normalized;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player") == true)
            {
                collision.gameObject.GetComponent<IDamage>().GetDamage();
            }
            if (collision.gameObject.CompareTag("Ground") == true)
            {
                Debug.Log("땅부딪");
                Destroy(this.gameObject);
            }
        }

    }

}