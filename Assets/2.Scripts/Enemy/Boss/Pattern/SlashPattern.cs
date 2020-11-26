using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class SlashPattern : Patterns
    {
        
        [SerializeField]
        private float _correctionValue = 180;
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


        public override IEnumerator Run()
        {
            float _time = 0;
            _projectile.SetActive(true);
            while (_time < _patternTime)
            {
                _time += Time.deltaTime;
                yield return null;
            }
            //시간끝나면
            _patternAni.Play("Defult");
            _projectile.SetActive(false);
            RotationReset();

        }

        public override void Play()
        {
            if (Random.Range(0, 2) == 0)
                _patternAni.Play("SlashRight");
            else
                _patternAni.Play("SlashLeft");
        }
    }
}