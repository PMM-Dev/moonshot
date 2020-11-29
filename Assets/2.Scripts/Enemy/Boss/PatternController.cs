using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public interface IPattern
    {
        IEnumerator Run();
    }
    public interface IAnimation
    {
        void Play();
    }


    public class PatternController : MonoBehaviour
    {
        [SerializeField]
        protected List<Patterns> _patternContainer;
        [SerializeField]
        protected Patterns _exhaustPatterns;
        [SerializeField]
        protected Animator _pattrenAni;
        [SerializeField]
        protected GameObject _player;
        [SerializeField]
        [Range(1f, 3f)]
        protected float _patternAfterDelay = 2;
        [SerializeField]
        [Range(1, 5)]
        protected int _exhaustPatternsCount = 5;
        [SerializeField]
        protected bool _isStartCoroutine = false;

        protected SoundHelper _soundHelper;
        protected List<Patterns> _patternContainerCopy = new List<Patterns>();
        protected Patterns _currentPattern;

        private int _count = 0;
        private int _random = 0;

        public static PatternController Instance;

        private void Awake()
        {
            Instance = this;
            _soundHelper = gameObject.transform.parent.gameObject.AddComponent<SoundHelper>();
        }

        private void Start()
        {
            for (int i = 0; i < _patternContainer.Count; i++)
            {
                _patternContainer[i].SoundHelper = _soundHelper;
                _patternContainer[i].Player = _player;
                _patternContainer[i].PatternAni = _pattrenAni;
                _patternContainerCopy.Add(_patternContainer[i]);
            }
            _exhaustPatterns.PatternAni = _pattrenAni;
            _exhaustPatterns.SoundHelper = _soundHelper;
            if (_player == null)
                _player = MainPlayerManager.Instance.Player;

            if (_isStartCoroutine)
                Appear();
        }

        public void Appear()
        {
            _pattrenAni.Play("Back");
            StartCoroutine(FiniteStateMachine());
        }


        void RandomCurrentPattern()
        {
            if (_patternContainerCopy.Count <= 0)
                for (int i = 0; i < _patternContainer.Count; i++)
                    _patternContainerCopy.Add(_patternContainer[i]);

            _random = Random.Range(0, _patternContainerCopy.Count);
            _currentPattern = _patternContainerCopy[_random];
            _patternContainerCopy.RemoveAt(_random);
        }
        void LaughingSound() {
            _pattrenAni.Play("Lauging");
            _soundHelper.PlaySound(false, "Boss_Laughing");
        }

        IEnumerator FiniteStateMachine()
        {
            _soundHelper.PlaySound(false, "Boss_Intro");
            Invoke("LaughingSound", 1f);
            yield return new WaitForSeconds(4f);
            while (true)
            {
                yield return new WaitForSeconds(_patternAfterDelay);
                RandomCurrentPattern();
                _currentPattern.Play();
                yield return StartCoroutine(_currentPattern.Run());
                _count++;
                if (_count >= _exhaustPatternsCount)
                {
                    yield return new WaitForSeconds(_patternAfterDelay);
                    _pattrenAni.Play("Defult");
                    yield return new WaitForSeconds(_patternAfterDelay);
                    _exhaustPatterns.Play();
                    yield return StartCoroutine(_exhaustPatterns.Run());

                    _count = 0;
                }
                yield return new WaitForSeconds(_patternAfterDelay);
            }
        }


    }
}
