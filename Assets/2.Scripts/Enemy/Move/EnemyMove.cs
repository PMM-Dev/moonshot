using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{

    public class EnemyMove : MonoBehaviour
    {
        protected GameObject _player;

        [SerializeField]
        private GameObject[] _wayPoints;
        [Tooltip("1초당 얼마나 가는가 거리/s")]
        [SerializeField]
        [Range(1, 10)]
        protected float _startSpeed = 2f;

        [Tooltip("플레이어 인식 범위 안보면 -1")]
        [SerializeField]
        protected float _lookingPlayerRange = 10f;

        [SerializeField]
        protected AnimationCurve _wayPointCurve;

        private float _requiredTime;
        private int _targetIndex = 1;
        private int _tempStartIndex = 0;

        protected Vector3 _targetDirction;
        protected Vector3 _originScale;
        protected Vector3 _reversedScale;
        protected Vector3 _playerDirction;
        protected Vector3 _startPosition;
        protected float _speed;
        protected float _maxSpeed = 10f;
        protected float _targetDistance;
        protected float _playerDistance = 9999f;
        protected float _time = 0;

        const float _playerCorrectionValue = 100 / 8;

        private void Start()
        {
            _speed = _startSpeed;
            _player = MainPlayerManager.Instance.Player;
            if (_wayPoints.Length > 0)
                this.transform.position = _wayPoints[_tempStartIndex].gameObject.transform.position;
            SetFlipSize();
            StartCoroutine(Translate());
        }


        virtual protected IEnumerator Translate()
        {
            _time = 0;
            if (_targetIndex >= _wayPoints.Length)
            {
                _targetIndex = 0;
                _tempStartIndex = _wayPoints.Length - 1;
            }

            _startPosition = this.gameObject.transform.position;
            CalculationDistance(_wayPoints[_targetIndex].transform.position);

            _requiredTime = _targetDistance / _speed;


            FlipSize();

            while (true)
            {
                _time += Time.deltaTime;
                if (_time > _requiredTime)
                    break;

                PlayerDistanceCalculation();

                if (_playerDistance < _lookingPlayerRange)
                    yield return StartCoroutine(TrackingPlayer());

                this.transform.position = Vector3.Lerp(_startPosition , _wayPoints[_targetIndex].transform.position, _wayPointCurve.Evaluate(_time/ _requiredTime));
                
                yield return null;
            }

            _targetIndex++;
            _tempStartIndex = _targetIndex - 1;

            //반복 시키기 위해 함.
            StartCoroutine(Translate());
        }

        virtual protected IEnumerator TrackingPlayer()
        {
            yield return null;
        }

        virtual protected void SetFlipSize()
        {
            _originScale = this.transform.localScale;
            _reversedScale = this.transform.localScale;
            _reversedScale.x *= -1;
        }

        public void CalculationDistance(Vector3 _targetPosition )
        {
            _targetDirction = (_targetPosition - this.gameObject.transform.position).normalized;
            _targetDistance = Vector3.Magnitude(this.gameObject.transform.position - _targetPosition);
        }

        virtual public void FlipSize() {

            if (_targetDirction.x > 0)
                this.transform.localScale = _reversedScale;
            else
                this.transform.localScale = _originScale;
        }

        protected void PlayerDistanceCalculation()
        {
            _playerDirction = _player.transform.position - this.gameObject.transform.position;
            _playerDirction.y += _player.transform.localScale.y * _playerCorrectionValue;
            _playerDistance = Vector3.Magnitude(_playerDirction);
            _playerDirction = _playerDirction.normalized;
        }

    }
}