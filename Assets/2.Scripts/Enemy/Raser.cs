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
        [SerializeField]
        private ParticleSystem[] _shotParticle = new ParticleSystem[2];
        //private bool _isWolf;
        [SerializeField]
        private float _builletInterver;
        [SerializeField]
        private float _recognitionRange = 10f;
        private float _time = 0;
        private GameObject _player;
        private float _playerDistance;
        protected SoundHelper _soundhelper;


        private void Start()
        {
            _soundhelper = this.gameObject.AddComponent<SoundHelper>();
            _player = MainPlayerManager.Instance.Player;

            for (int i = 0; i < _shotParticle.Length; i++)
                _shotParticle[i].Stop();

        }

        private void Update()
        {
            PlayerDistanceCalculation();
            if (_playerDistance > _recognitionRange)
                return;
            _time += Time.deltaTime;
            if (_time > _builletInterver)
            {
                CreateBullet();
                _soundhelper.PlaySound(false, "ShootLazer");
            }
        }

        private void PlayerDistanceCalculation()
        {
            _playerDistance = Vector3.Magnitude(_player.transform.position - this.gameObject.transform.position);
        }

        private void CreateBullet()
        {
            if (_isRabbit == true && (_player.transform.position.y +1  > this.transform.position.y))
                return;

            for (int i = 0; i < _shotParticle.Length; i++)
                _shotParticle[i].Play();

            GameObject bullet = Instantiate(_bulletPrefabs, _shotParticle[0].gameObject.transform.position, transform.rotation);
            _time = 0;
        }
    }
}