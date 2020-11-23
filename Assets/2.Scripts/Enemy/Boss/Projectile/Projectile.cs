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
        [Range(7, 10)]
        protected float _triggerDistance = 7f;

        protected float _correctionValue = 180f;
        protected float _projectileSpeed = 4f;
        protected Vector3 _positionVector3;
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
            _positionVector3 = this.gameObject.transform.position;
            SetPosition();
        }

        protected void Rotate() {
            this.transform.Rotate(Vector3.forward * Time.deltaTime * _rotationSpeed * _correctionValue);
        }

        protected void SetPosition()
        {
            _positionVector3.y = this.transform.position.y;
            _positionVector3.x = _player.transform.position.x;
            this.transform.position = _positionVector3;
        }

        protected void Run()
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
