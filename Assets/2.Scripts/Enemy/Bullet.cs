using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _disapperTime = 3f;
        [SerializeField] private bool _isVetical;
        public bool isVetical
        {
            set
            {
                _isVetical = value;
            }
        }
        [SerializeField] private Transform _transform;

        private void Update()
        {
            if (_isVetical == true)
                transform.Translate(Vector3.down * _bulletSpeed * Time.smoothDeltaTime, Space.World);
            else
                transform.Translate(new Vector3(-this.transform.localScale.x,0,0) * _bulletSpeed * 10 * Time.smoothDeltaTime, Space.World);

            Destroy(this.gameObject, _disapperTime);
        }
    }

}