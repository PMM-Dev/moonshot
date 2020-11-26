using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Hat : Projectile
    {
        [SerializeField]
        [Range(0, 1)]
        protected new float _correctionValue = 0.5f;
        [SerializeField]
        protected AnimationCurve _hatCurve;
        Vector3 _upPosition;
        Vector3 _downPosition;

        private void Start()
        {

            if (_player == null)
                _player = MainPlayerManager.Instance.Player;
            if (_player != null)
                TargetPlayerPosition();
            SettingPosition();
            SetDownPosition();
            //StartCoroutine(Down());
        }

        private void SettingPosition()
        {
            _parentPosition = this.transform.parent.transform.position;
            _downPosition = this.transform.position;
            _upPosition = this.transform.position;
            _upPosition = _parentPosition;
            _downPosition = _parentPosition;
            _downPosition.y -= (_parentPosition.y * _triggerDistance);
        }

        private void Update()
        {
            Rotate();
        }

        private void SetDownPosition()
        {
            _upPosition.x = _player.transform.position.x;
            _downPosition.x = _player.transform.position.x;
        }


        public IEnumerator Down()
        {
            float _time = 0;


            while (true)
            {
                _time += Time.deltaTime * _correctionValue;
                if (_time > 1f)
                    break;

                this.transform.position = Vector3.Lerp(_upPosition, _downPosition, _hatCurve.Evaluate(_time));

                yield return null;
            }
            SetDownPosition();
            yield return StartCoroutine(Up());
            this.gameObject.SetActive(false);
        }

        IEnumerator Up()
        {
            float _time = 0;
            while (true)
            {
                _time += Time.deltaTime * _correctionValue;
                if (_time > 1f)
                    break;

                this.transform.position = Vector3.Lerp( _downPosition, _upPosition, _hatCurve.Evaluate(_time));

                yield return null;
            }
            yield return null;
        }


    }
}
