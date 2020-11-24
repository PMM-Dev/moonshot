using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class SlashPattern : Patterns
    {
        [SerializeField]
        [Range(1, 5)]
        private float _rotationSpeed;
        [SerializeField]
        private float _correctionValue = 180;
        [SerializeField]
        private float _maxAngle = 120;
        private float _accumulate = 0;
        private Vector3 _originParentScale;
        private Vector3 _reverceParentScale;
        private Quaternion _originParentRotation;

        private void Awake()
        {
            _originParentRotation = this.transform.rotation;
            _originParentScale = this.transform.localScale;
            _reverceParentScale = this.transform.localScale;
            _reverceParentScale.x *= -1;
        }
        

        private void RotationReset()
        {
            this.transform.localScale = _originParentScale;
            this.transform.rotation = _originParentRotation;
        }


        protected void RightSlash()
        {

            this.transform.localScale = _reverceParentScale;
            this.transform.Rotate(Vector3.back * Time.deltaTime * _rotationSpeed * _correctionValue);
            _accumulate += Time.deltaTime * _rotationSpeed * _correctionValue;
        }

        protected void LeftSlash()
        {
            this.transform.Rotate(Vector3.forward * Time.deltaTime * _rotationSpeed * _correctionValue);
            _accumulate += Time.deltaTime * _rotationSpeed * _correctionValue;
        }

        public override IEnumerator Run()
        {
            _projectile.SetActive(true);
            _accumulate = 0;
            if (Random.Range(0, 2) == 0)
            {
                this.transform.localScale = _reverceParentScale;
                while (_accumulate < _maxAngle)
                {
                    RightSlash();
                    yield return null;
                }
                //애니메이션 스타트
            }
            else
            {

                //애니메이션 스타트
                while (_accumulate < _maxAngle)
                {
                    LeftSlash();
                    yield return null;
                }
            }
            //시간끝나면

            _projectile.SetActive(false);
            RotationReset();
        }
    }
}