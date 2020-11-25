using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Raser : MonoBehaviour
    {
        [Tooltip("늑대와 토끼에 맞는 불릿 넣기")] [SerializeField]
        private GameObject _bulletPrefabs;
        [Tooltip("토끼면 체크")] [SerializeField]
        private bool _isRabbit;
        //private bool _isWolf;
        [SerializeField]
        private float _builletInterver;
        [SerializeField]
        private float _recognitionRange = 10f;
        private float _time = 0;
        private bool _stop = false;
        private GameObject _player;
        private float _playerDistance;


        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            PlayerDistanceCalculation();
            if (_playerDistance > _recognitionRange)
                return;
            _time += Time.deltaTime;
            if (_time > _builletInterver)
                CreateBullet();
        }

        void PlayerDistanceCalculation()
        {
            _playerDistance = Vector3.Magnitude(_player.transform.position - this.gameObject.transform.position);
        }
        void CreateBullet()
        {
            if (_isRabbit == true && (_player.transform.position.y +1  > this.transform.position.y))
                return;
            GameObject bullet = Instantiate(_bulletPrefabs, transform.position, transform.rotation);
            
            _time = 0;
        }
    }
}