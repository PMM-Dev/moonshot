using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{

    public class TestMove : MonoBehaviour
    {
        protected GameObject _player;
        protected float _WayPointDistance;
        protected float _playerDistance = 9999f;

        [Tooltip("시계방향으로 배치해야지 잘 작동됨.")]
        [SerializeField]
        private GameObject[] _wayPoints;
        [Tooltip("1초당 얼마나 가는가 거리/s")]
        [SerializeField]
        [Range(1, 10)]
        private float _speed = 2f;
        [SerializeField]
        [Range(2, 5)]
        private float _trakingSpeed = 3f;

        [Header("Option")]
        [Tooltip("선형 참조/원형 참조")]
        [SerializeField]
        private bool _isRound = false;
        [Tooltip("플레이어 인식 범위 안보면 -1")]
        [SerializeField]
        private float _DetectingPlayerRange = 10f;


        [Header("굼뱅이 전용")]
        [Tooltip("이동시 좌루만 보는지 여부")]
        [SerializeField]
        private bool _isYAxisLook = false;

        [Header("늑대인간 전용")]
        [Tooltip("움직이는지 여부")]
        [SerializeField]
        private bool _isMove = true;
        [Tooltip("플레이어 보는지 여부")]
        [SerializeField]
        private bool _isLookPlayer = false;

        [Header("마스피플 전용")]
        [Tooltip("플레이어 따라가는지 여부")]
        [SerializeField]
        private bool _isTrakingPlayer = false;
        private GameObject _targetWayPointTarget;
        private Vector3 _originScale;
        private Vector3 _reversedScale;
        private Vector3 _wayPointDirction;
        private Vector3 _playerDirction;
        private float _playerCorrectionValue = 100 / 8;
        private int _targetIndex = 0;
        private int _tempStartIndex;
        private int _indexAdd = 1;
        private bool _isturn = true;
        public AnimationCurve _wayPointCurve;
        public AnimationCurve _trakingPlayerCurve;
        public float _time = 0;
        [SerializeField]
        [Range(0, 1)]
        public float _value = 1;
        private float _requiredTime;
        private void Start()
        {
            _player = MainPlayerManager.Instance.Player;
            _originScale = this.transform.localScale;
            _reversedScale = this.transform.localScale;
            _reversedScale.x *= -1;
            if (_wayPoints.Length > 0)
                _targetWayPointTarget = _wayPoints[0];
            _tempStartIndex = _wayPoints.Length - 1;
            this.transform.position = _wayPoints[_tempStartIndex].gameObject.transform.position;
            StartCoroutine(Translate());

        }


        private void Update()
        {
            /*
            
            //웨이포인트 거리 계산
            if (_wayPoints.Length > 0)
                PhysicalCalculation();
            //플레이어와 거리 계산
            if (_player != null)
                PlayerDistanceCalculation();
            if (_isMove == true)
            {
            //플레이어를 따라감
                if (_playerDistance < _DetectingPlayerRange && _isTrakingPlayer == true)
                    TrackingPlayer();
            //웨이포인트 따라감.
                else
                    MoveToWayPoint();
            }
            */
        }
        IEnumerator Translate()
        {
            if (_targetIndex >= _wayPoints.Length)
            {
                _targetIndex = 0;
                _tempStartIndex = _wayPoints.Length - 1;
            }
            CalculationWayPointsn();

            _time = 0;
            _requiredTime = _WayPointDistance / _speed;


            FlipSize();

            while (true)
            {
                _time += Time.deltaTime * _speed;

                if (_time > 1f)
                    break;
                PlayerDistanceCalculation();

                if (_playerDistance < _DetectingPlayerRange)
                    yield return StartCoroutine(IETrackingPlayer());

                this.transform.position = Vector3.Lerp(_wayPoints[_tempStartIndex].transform.position, _wayPoints[_targetIndex].transform.position, _wayPointCurve.Evaluate(_time));
                
                yield return null;
            }

            _targetIndex++;
            _tempStartIndex = _targetIndex - 1;
            
            //반복 시키기 위해 함.
            StartCoroutine(Translate());
        }

        IEnumerator IETrackingPlayer()
        {
            Debug.Log("find Player");

            _time = 0;
            Vector3 _tempStartPosition = this.transform.position;


            while (true)
            {

                transform.Translate(_playerDirction * _trakingSpeed * Time.smoothDeltaTime, Space.World);
                /*
                if (_time < 1f)
                    _time += Time.deltaTime;
                this.transform.position = Vector3.Lerp(this.transform.position, _player.transform.position, 0.2f);
                */

                yield return null;
                PlayerDistanceCalculation();
                if (_playerDistance > _DetectingPlayerRange)
                {
                    yield return StartCoroutine(BackWayPoint());
                    break;
                }
            }
        }
        IEnumerator BackWayPoint()
        {
             _time = 0;
            Vector3 _TmpStartPosition = this.transform.position;

            FlipSize();

            while (true)
            {
                _time += Time.deltaTime * _speed;

                if (_time > 1f)
                    break;
                PlayerDistanceCalculation();
                if (_playerDistance < _DetectingPlayerRange)
                    yield return StartCoroutine(IETrackingPlayer());

                this.transform.position = Vector3.Lerp(_TmpStartPosition, _wayPoints[_targetIndex].transform.position, _wayPointCurve.Evaluate(_time));
                yield return null;
            }

            _targetIndex++;
            _tempStartIndex = _targetIndex - 1;

            StartCoroutine(Translate());
        }

        public void CalculationWayPointsn()
        {
            _wayPointDirction = (_wayPoints[_targetIndex].transform.position - this.gameObject.transform.position).normalized;
            _WayPointDistance = Vector3.Magnitude(_wayPoints[_tempStartIndex].transform.position - _wayPoints[_targetIndex].transform.position);
        }

        public void FlipSize() {

            if (_wayPointDirction.x > 0)
                this.transform.localScale = _reversedScale;
            else
                this.transform.localScale = _originScale;
        }

        void PlayerDistanceCalculation()
        {
            _playerDirction = _player.transform.position - this.gameObject.transform.position;
            _playerDirction.y += _player.transform.localScale.y * _playerCorrectionValue;
            _playerDistance = Vector3.Magnitude(_playerDirction);
            _playerDirction = _playerDirction.normalized;
        }



        /*------------------------------------------------------------------------
         ------------------------------이전 move함수.-----------------------------
         ------------------------------------------------------------------------*/


        void LookPlayer()
        {
            if (_isturn == true)
            {
                if (_playerDirction.x > 0)
                {
                    this.transform.localScale = _reversedScale;
                }
                else
                {
                    this.transform.localScale = _originScale;
                }
            }
        }

        //타겟의 x좌표를 보게 하는 함수
        void LookTarget()
        {
            Vector3 _tmpScale = _originScale;

            if (_isYAxisLook == false)
            {
                if (_isturn == true)
                {
                    if (_wayPointDirction.x > 0)
                    {
                        _tmpScale = _reversedScale;
                    }
                    else
                    {
                        _tmpScale = _originScale;
                    }

                }
            }
            else
            {
                float _angle = Mathf.Atan2(_wayPointDirction.y, _wayPointDirction.x) * Mathf.Rad2Deg;
                _tmpScale = _reversedScale;
                transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            }
            this.transform.localScale = _tmpScale;
        }

        //거리와, 방향을 계산하는 함수
        void PhysicalCalculation()
        {
            _wayPointDirction = (_targetWayPointTarget.transform.position - this.gameObject.transform.position).normalized;
            _WayPointDistance = Vector3.Magnitude(this.gameObject.transform.position - _targetWayPointTarget.transform.position);
        }

        //타겟을 따라 움직이는 함수
        //타겟과 근접하면 타겟을 바꿔줌
        void MoveToWayPoint()
        {
            transform.Translate(_wayPointDirction * _speed * Time.smoothDeltaTime, Space.World);

            if (_WayPointDistance < 0.5f && _isturn == true)
            {
                ChangeTarget();
            }

        }

        //플레이어 따라가기만 하는 함수
       /* void TrackingPlayer()
        {
            transform.Translate(_playerDirction * _speed * Time.smoothDeltaTime, Space.World);
        }*/

        //타겟을 적절하게 바꿔줌
        //만약 끝이나 처음에 도달하면 역순/정순으로 다시 돌아다님
        void ChangeTarget()
        {
            if (_isRound == false)
            {
                if (_targetIndex <= 0)
                {
                    _indexAdd = 1;
                }
                else if (_targetIndex >= _wayPoints.Length - 1)
                {
                    _indexAdd = -1;
                }
            }
            else
            {
                if (_targetIndex >= _wayPoints.Length - 1)
                    _targetIndex = -1;
            }
            _targetIndex += _indexAdd;
            _targetWayPointTarget = _wayPoints[_targetIndex];
        }

    }
}