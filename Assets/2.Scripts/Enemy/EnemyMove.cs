using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy{

    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private GameObject[] _wayPoints;
        [SerializeField] private float _speed = 1;
        private GameObject _target;
        private Vector3 _dirction;
        private float _distance;
        private int _targetIndex;
        private int _indexAdd = 1;

        private void Start()
        {
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
            if (_dirction.x > 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            { 
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
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

            if (_distance < 0.2f) {
                ChangeTarget();
            }

        }

        //타겟을 적절하게 바꿔줌
        //만약 끝이나 처음에 도달하면 역순/정순으로 다시 돌아다님
        void ChangeTarget() {
            if (_targetIndex <= 0)
            {
                _indexAdd = 1;
            }
            else if(_targetIndex >= _wayPoints.Length)
            {
                _indexAdd = -1;
            }
            _targetIndex += _indexAdd;


            _target = _wayPoints[_targetIndex];
        }


    }
}