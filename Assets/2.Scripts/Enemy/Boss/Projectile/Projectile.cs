using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 4)]
        protected float _rotationSpeed = 1f;

        [SerializeField]
        [Range(2, 10)]
        protected float _triggerDistance = 7f;

        protected float _correctionValue = 180f;
        protected float _projectileSpeed = 4f;
        protected Vector3 _startPosition;
        protected GameObject _player;

        public float ProjectileSpeed
        {
            set { _projectileSpeed = value; }
        }
        //플레이어는 호출하는 객체에서 받기로..
        public GameObject Player
        {
            set { _player = value; }
        }

        protected void Start()
        {
            if (_player == null)
                _player = MainPlayerManager.Instance.Player;
            _startPosition = this.transform.transform.position;
            if (_player != null)
                TargetPlayerPosition();
        }

        protected void ChiledRotate()
        {
            this.transform.GetChild(0).gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * _rotationSpeed * _correctionValue);
        }

        protected void Rotate() {
            this.transform.Rotate(Vector3.forward * Time.deltaTime * _rotationSpeed * _correctionValue);
        }

        public void TargetPlayerPosition(bool reset = false)
        {
            if (_player == null)
            {
                _player = GameObject.FindWithTag("Player");
            }

            if (reset == true)
                _startPosition.y = this.transform.position.y;
            else if (this.transform.parent != null)
                _startPosition.y = this.transform.parent.position.y + 1f;
            _startPosition.x = _player.transform.position.x;
            this.transform.position = _startPosition;
        }

        protected virtual void Run()
        {
            this.transform.Translate(Vector3.down * _projectileSpeed * Time.smoothDeltaTime, Space.World);

            if (this.transform.position.y <= _player.transform.position.y - _triggerDistance)
            {
                Pattern();
            }
        }


        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") == true)
            {
                collision.gameObject.GetComponent<IDamage>().GetDamage();
            }
        }

        virtual protected void Pattern() { }
    }


}
