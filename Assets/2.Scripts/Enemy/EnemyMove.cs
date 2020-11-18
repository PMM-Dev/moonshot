using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy{

    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private GameObject[] _wayPoints;
        [SerializeField] private float _speed = 1;

        [Header("Option")]
        [Tooltip ("선형 참조/원형 참조")][SerializeField] private bool _isRound = false;
        [Tooltip("이동시 좌루만 보는지 목표를 보는지 여부")] [SerializeField] private bool _isYAxisLook = false;
        [Tooltip("회전할때 상하 반전 여부")] [SerializeField] private bool _isYAxisReverce = false;
        private GameObject _target;
        Vector3 _originScale;
        Vector3 _reversedScale;
        private Vector3 _dirction;
        private float _distance;
        private int _targetIndex;
        private int _indexAdd = 1;

        private void Start()
        {
            _originScale = this.transform.localScale;
            _reversedScale = this.transform.localScale;
            _reversedScale.x *= -1;
            if (_isYAxisReverce == true)
                _reversedScale.y *= -1;
            _target = _wayPoints[0];
        }

        private void Update()
        {
            PhysicalCalculation();
            MoveToWayPoint();
            LookTarget();
            
        }

        //타겟의 x좌표를 보게 하는 함수
        void LookTarget() {
            Vector3 _tmpScale = _originScale;

            if (_isYAxisLook == false)
            {
                if (_dirction.x > 0)
                {
                    _tmpScale = _reversedScale;
                }
            }
            else
            {
                float _angle = Mathf.Atan2(_dirction.y,_dirction.x)* Mathf.Rad2Deg;
                if (_angle < 0)
                {
                    _angle += 180;
                }
                else
                {
                    _tmpScale = _reversedScale;
                }
                transform.rotation = Quaternion.AngleAxis(_angle,Vector3.forward);
            }
                this.transform.localScale = _tmpScale;
        }

        //거리와, 방향을 계산하는 함수
        void PhysicalCalculation() {
            _dirction = _target.transform.localPosition  - this.gameObject.transform.localPosition;
            _dirction = _dirction.normalized;
            _distance = Vector3.Magnitude(this.gameObject.transform.localPosition - _target.transform.localPosition);
        }

        //타겟을 따라 움직이는 함수
        //타겟과 근접하면 타겟을 바꿔줌
        void MoveToWayPoint()
        {
            transform.Translate(_dirction * _speed * Time.smoothDeltaTime, Space.World);

            if (_distance < 0.5f) {
                ChangeTarget();
            }

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
            _target = _wayPoints[_targetIndex];
        }
    }
}