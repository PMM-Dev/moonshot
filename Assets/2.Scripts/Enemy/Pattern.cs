using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy{
    public class Pattern : MonoBehaviour
    {
        [SerializeField]int _pattenDelay;
        [SerializeField] float _rage;
        private GameObject _target;
        float _distance;//차후에 move에서 받아오기

        private void Update()
        {
            PhysicalCalculation();
        }


        //거리와, 방향을 계산하는 함수
        void PhysicalCalculation()
        {
            _distance = Vector3.Magnitude(this.gameObject.transform.localPosition - _target.transform.localPosition);
        }

    }

    
}