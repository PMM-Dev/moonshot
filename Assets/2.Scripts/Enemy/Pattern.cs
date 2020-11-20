using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy{
    public class Pattern : MonoBehaviour
    {
        protected GameObject _player;
        protected float _playerDistance = 9999f;

        [SerializeField]private float _patternDelay;
        [SerializeField] protected float _patternRage;
        [Tooltip("패턴 딜레이를 포함한 쿨타임")][SerializeField] protected float _patternCollTime;
        private bool _isCanPattern = true;
        //float _time = 0; 2번 패턴 사용시 필요
        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (_player != null)
                PlayerDistanceCalculation();
            if (_playerDistance < _patternRage)
                AttackPattern();
        }

        void PlayerDistanceCalculation()
        {
            _playerDistance = Vector3.Magnitude(_player.transform.localPosition - this.gameObject.transform.localPosition);
        }


        //패턴을 실행시키는 함수
        //아래 두가지 보고 피드백좀
        //코루틴은 프레임,  함수는 시간으로 알고있음
        //일단은 코루틴이 프레임이기 때문에 퍼포먼스적으로 좋다고 생각해서 코루틴 사용함

        void AttackPattern() {
            if (_isCanPattern == true)
            {
                StartCoroutine(PatternCoolTime());
                StartCoroutine(Pattern1());
            }

        }

        /*
        void AttackPattern2() {
            _time +=Time.deltaTime;
            //에니메이팅 스타트
            if (_time >= _patternDelay)
            {
                _time = 0;
                ActuallyPattern();
            }
        }*/

        IEnumerator Pattern1() {
            //애니메이팅 스타트 단 애니메이션은 _patternDelay랑 길이가 같아야함
            Debug.Log("player Find");
            yield return new WaitForSeconds(_patternDelay);
            ActuallyPattern();
        }

        IEnumerator PatternCoolTime()
        {
            _isCanPattern = false;
            yield return new WaitForSeconds(_patternCollTime);
            _isCanPattern = true;

        }

        //overriding Actually pattern
        virtual protected void ActuallyPattern() {
            
        }


    }

    
}