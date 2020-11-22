using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _disapperTime = 3f;
        [SerializeField] private bool _isVertical;
        public bool IsVertical
        {
            set
            {
                _isVertical = value;
            }
        }

        private void Update()
        {
            if (_isVertical == true)
                transform.Translate(Vector3.down * _bulletSpeed * Time.smoothDeltaTime, Space.World);
            else
                transform.Translate(new Vector3(-this.transform.localScale.x,0,0) * _bulletSpeed * 10 * Time.smoothDeltaTime, Space.World);

            Destroy(this.gameObject, _disapperTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") == true) {
                collision.gameObject.GetComponent<IDamage>().GetDamage();
            }
        }
    }

}