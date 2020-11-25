using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public interface IPattern
    {
        IEnumerator Run();
    }


    public class PatternController : MonoBehaviour
    {
        [SerializeField]
        protected List<Patterns> _patternContainer;
        [SerializeField]
        protected Patterns _exhaustPatterns;
        [SerializeField]
        protected GameObject _player;
        [SerializeField]
        [Range(1, 5)]
        protected int _patternAfterDelay = 2;

        protected List<Patterns> _patternContainerCopy = new List<Patterns>();
        protected Patterns _currentPattern;

        private int _count = 0;
        private int _random = 0;

        private void Awake()
        {
            for (int i = 0; i < _patternContainer.Count; i++)
            {
                _patternContainer[i].Player = _player;
                _patternContainerCopy.Add(_patternContainer[i]);
            }

            if (_player == null)
                _player = MainPlayerManager.Instance.Player;

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

        IEnumerator FiniteStateMachine()
        {
            while (true)
            {
                //애니메이션
                RandomCurrentPattern();
                yield return StartCoroutine(_currentPattern.Run());
                _count++;
                if (_count >= 5)
                {
                    yield return new WaitForSeconds(_patternAfterDelay);
                    yield return StartCoroutine(_exhaustPatterns.Run());
                    _count = 0;
                }
                yield return new WaitForSeconds(_patternAfterDelay);
            }
        }
    }
}
