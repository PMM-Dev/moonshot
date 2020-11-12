using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy{

    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] GameObject[] wayPoints;
        [SerializeField] GameObject target;
        [SerializeField] float speed = 1;
        Vector3 dirction;
        float distance;
        int targetIndex;
        int indexAdd = 1;

        private void Start()
        {
            target = wayPoints[0];
        }

        private void Update()
        {
            PhysicalCalculation();
            MoveToWayPoint();
            LookTarget();
        }

        //타겟의 x좌표를 보게 하는 함수
        void LookTarget() {
            if (dirction.x > 0)
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
            dirction = target.transform.localPosition  - this.gameObject.transform.localPosition;
            dirction = dirction.normalized;
            distance = Vector3.Magnitude(this.gameObject.transform.localPosition - target.transform.localPosition);
        }

        //타겟을 따라 움직이는 함수
        //타겟과 근접하면 타겟을 바꿔줌
        void MoveToWayPoint()
        {
            Debug.Log(dirction +" "+ speed);
            transform.Translate(dirction * speed * Time.smoothDeltaTime, Space.World);

            if (distance < 0.2f) {
                ChangeTarget();
            }

        }

        //타겟을 적절하게 바꿔줌
        //만약 끝이나 처음에 도달하면 역순/정순으로 다시 돌아다님
        void ChangeTarget() {
            if (targetIndex <= 0)
            {
                indexAdd = 1;
            }
            else if(targetIndex >= wayPoints.Length)
            {
                indexAdd = -1;
            }
            targetIndex += indexAdd;


            target = wayPoints[targetIndex];
        }


    }
}