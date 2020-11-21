using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy{

    public class EnemyMove : MonoBehaviour
    {
        protected GameObject _player;
        protected float _WayPointDistance;
        protected float _playerDistance;

        [Tooltip("시계방향으로 배치해야지 잘 작동됨.")] [SerializeField] private GameObject[] _wayPoints;
        [SerializeField] private float _speed = 1;

        [Header("Option")]
        [Tooltip("선형 참조/원형 참조")] [SerializeField] private bool _isRound = false;
        [Tooltip("이동시 좌루만 보는지 여부")] [SerializeField] private bool _isYAxisLook = false;
        [Tooltip("플레이어 인식 범위 안보면 -1")] [SerializeField] private float _DetectingPlayerRange = 10f;

        [Header("늑대인간 전용")]
        [Tooltip("움직이는지 여부")] [SerializeField] private bool _isMove = true;
        [Tooltip("플레이어 보는지 여부")] [SerializeField] private bool _isLookPlayer = false;

        [Header("마스피플 전용")]
        [Tooltip("플레이어 따라가는지 여부")] [SerializeField] private bool _isTrakingPlayer = false;
        [SerializeField] private GameObject _targetWayPointTarget;
        Vector3 _originScale;
        Vector3 _reversedScale;
        private Vector3 _wayPointDirction;
        private Vector3 _playerDirction;
        private int _targetIndex;
        private int _indexAdd = 1;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _originScale = this.transform.localScale;
            _reversedScale = this.transform.localScale;
            _reversedScale.x *= -1;
            _targetWayPointTarget = _wayPoints[0];
        }


        private void Update()
        {
            PhysicalCalculation();
            PlayerDistanceCalculation();
            if (_isMove == true)
            {
                if (_playerDistance < _DetectingPlayerRange && _isTrakingPlayer == true)
                    TrackingPlayer();
                else
                    MoveToWayPoint();
            }

            if (_playerDistance < _DetectingPlayerRange && _isLookPlayer == true)
            {
                LookPlayer();
            }
            else
            {
                LookTarget();
            }
            
        }

        void PlayerDistanceCalculation()
        {
            _playerDirction = _player.transform.localPosition - this.gameObject.transform.localPosition;
            _playerDistance = Vector3.Magnitude(_playerDirction);
            _playerDirction = _playerDirction.normalized;
        }

        void LookPlayer()
        {
            Debug.Log(_playerDirction.x);
            if (_playerDirction.x > 0)
            {
                Debug.Log("reversed");
                this.transform.localScale = _reversedScale;
            }
            else
            {
                Debug.Log("origing");
                this.transform.localScale = _originScale;
            }
        }

        //타겟의 x좌표를 보게 하는 함수
        void LookTarget() {
            Vector3 _tmpScale = _originScale;

            if (_isYAxisLook == false)
            {
                if (_wayPointDirction.x > 0)
                {
                    _tmpScale = _reversedScale;
                }
                else {
                    _tmpScale = _originScale;
                }
            }
            else
            {
                float _angle = Mathf.Atan2(_wayPointDirction.y,_wayPointDirction.x)* Mathf.Rad2Deg;
                    _tmpScale = _reversedScale;
                transform.rotation = Quaternion.AngleAxis(_angle,Vector3.forward);
            }
                this.transform.localScale = _tmpScale;
        }

        //거리와, 방향을 계산하는 함수
        void PhysicalCalculation() {
            _wayPointDirction = _targetWayPointTarget.transform.localPosition  - this.gameObject.transform.localPosition;
            _wayPointDirction = _wayPointDirction.normalized;
            _WayPointDistance = Vector3.Magnitude(this.gameObject.transform.localPosition - _targetWayPointTarget.transform.localPosition);
        }

        //타겟을 따라 움직이는 함수
        //타겟과 근접하면 타겟을 바꿔줌
        void MoveToWayPoint()
        {
            transform.Translate(_wayPointDirction * _speed * Time.smoothDeltaTime, Space.World);

            if (_WayPointDistance < 0.5f) {
                ChangeTarget();
            }

        }

        //플레이어 따라가기만 하는 함수
        void TrackingPlayer() {
            transform.Translate(_playerDirction * _speed * Time.smoothDeltaTime, Space.World);
        }

        //타겟을 적절하게 바꿔줌
        //만약 끝이나 처음에 도달하면 역순/정순으로 다시 돌아다님
        void ChangeTarget() {
            if (_isRound == false)
            {
                if (_targetIndex <= 0)
                {
                    _indexAdd = 1;
                }
                else if (_targetIndex >= _wayPoints.Length -1)
                {
                    _indexAdd = -1;
                }
            }
            else
            {
                if (_targetIndex >= _wayPoints.Length-1)
                    _targetIndex = -1;
            }
            _targetIndex += _indexAdd;
            _targetWayPointTarget = _wayPoints[_targetIndex];
        }
    }
}