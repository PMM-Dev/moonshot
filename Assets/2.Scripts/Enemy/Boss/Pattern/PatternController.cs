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
        [Tooltip("d")][SerializeField]
        protected List<Patterns> _patternContainer;
        [SerializeField]
        protected Patterns _exhaustPatterns;
        [SerializeField]
        int a;

        protected List<Patterns> _patternContainerCopy = new List<Patterns>();
        protected Patterns _currentPattern;

        private int _count = 0;
        private int _random = 0;

        private void Start()
        {

            for (int i = 0; i < _patternContainer.Count; i++)
            {
                _patternContainerCopy.Add(_patternContainer[i]);
            }
            StartCoroutine(FiniteStateMachine());
        }

        void RandomCurrentPattern() {
            if (_patternContainerCopy.Count <= 0)
                ///    _patternContainerCopy = _patternContainer;
                ///    
                for (int i = 0; i < _patternContainer.Count; i++)
                    _patternContainerCopy.Add(_patternContainer[i]);
            else
            {
                _random = Random.Range(0, _patternContainerCopy.Count);
                _currentPattern = _patternContainerCopy[_random];
                _patternContainerCopy.RemoveAt(_random);
            }
        }

        IEnumerator FiniteStateMachine()
        {
            RandomCurrentPattern();
            while (true)
            {
                RandomCurrentPattern();
                //애니메이션
                yield return StartCoroutine(_currentPattern.Run());
                _count++;
                if (_count >= 5) {
                    //_exhaustPatterns.Run();
                    _count = 0;
                }
            }
        }
    }
}
