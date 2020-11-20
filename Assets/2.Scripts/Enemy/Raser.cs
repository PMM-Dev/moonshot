using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Raser : MonoBehaviour
    {
        [Tooltip("늑대와 토끼에 맞는 불릿 넣기")] [SerializeField] GameObject _bullitPrefabs;
        [Tooltip("늑대면 체크")] [SerializeField] bool _isWolf;
        [SerializeField] float _builletInterber;
        Vector3 _localScalse;
        float time = 0;

        private void Update()
        {
            time += Time.deltaTime;
            if (time > _builletInterber)
                MakingBullet();

        }

        void MakingBullet()
        {
            Debug.Log("생성");
            GameObject _bullet = Instantiate(_bullitPrefabs, transform.position, transform.rotation);
            _localScalse = _bullet.transform.localScale;
            if (_isWolf == true)
            {
                _bullet.GetComponent<Bullet>().isVetical = false;
                if (this.gameObject.transform.localScale.x < 0)
                    _localScalse.x *= -1;
                _bullet.transform.localScale = _localScalse;

            }
            time = 0;
        }
    }
}