using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy{
    public class Pattern : MonoBehaviour
    {
        [SerializeField]
        protected Animator _patternAni;
        [SerializeField]
        private float _patternDelay;
        [SerializeField]
        protected float _patternRage;
        [Tooltip("패턴 딜레이를 포함한 쿨타임")][SerializeField]
        protected float _patternCollTime;
        private bool _isCanPattern = true;
        [SerializeField]
        protected GameObject _player;
        protected float _playerDistance = 9999f;
        [Tooltip("굼뱅이는 체크 해제 해줘야함")]
        [SerializeField]
        protected bool _isHaveSound = true;
        protected SoundHelper _soundhelper;
        //float _time = 0; 2번 패턴 사용시 필요

        private void Start()
        {
            _player = MainPlayerManager.Instance.Player;
            if (_soundhelper == null && _isHaveSound == true)
                _soundhelper = this.gameObject.AddComponent<SoundHelper>();
            _isCanPattern = true;
        }

        private void Update()
        {
            if (_player != null)
                PlayerDistanceCalculation();
            if (_playerDistance < _patternRage)
                AttackPattern();
        }

        private void OnEnable()
        {
            if (_player != null)
            {
                PlayerDistanceCalculation();
                _player = MainPlayerManager.Instance.Player;
            }
            if (_soundhelper != null)
                _soundhelper = this.gameObject.AddComponent<SoundHelper>();
            _isCanPattern = true;

        }

        void PlayerDistanceCalculation()
        {
            _playerDistance = Vector3.Magnitude(_player.transform.position - this.gameObject.transform.position);
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

        IEnumerator Pattern1() {
            //애니메이팅 스타트 단 애니메이션은 _patternDelay랑 길이가 같아야함
            
            Animation();
            yield return new WaitForSeconds(_patternDelay);
            ActuallyPattern();
        }

        IEnumerator PatternCoolTime()
        {
            _isCanPattern = false;
            yield return new WaitForSeconds(_patternCollTime);
            _isCanPattern = true;

        }

        virtual protected void Animation() { _patternAni.Play("Pattern"); }

        //overriding Actually pattern
        virtual protected void ActuallyPattern() {
            
        }


    }

    
}