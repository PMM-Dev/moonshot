using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class SingPattern : Patterns
    {
        [Header("몹 할당(Life를 가진 본체만 할당해줘야함.)")]
        [SerializeField]
        Life[] _rabbits;
        [SerializeField]
        Life[] _masPeoples;
        [Header("전체 몹값보다 충분히 낮게 잡지 않으면 겜 멈춤")]
        [SerializeField]
        int _maxSpownCount;

        int _random;
        private int _spwancount;
        private int _avoidInfiniteLoops;

        private void Start()
        {
            for (int i = 0; i < _rabbits.Length; i++)
            {
                _rabbits[i].gameObject.transform.parent.gameObject.SetActive(true);
                _rabbits[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < _masPeoples.Length; i++)
            {
                _masPeoples[i].gameObject.transform.parent.gameObject.SetActive(true);
                _masPeoples[i].gameObject.SetActive(false);
            }
        }

        public override IEnumerator Run()
        {
            _soundHelper.PlaySound(false, "Boss_Sing");

            _spwancount = 0;
            _avoidInfiniteLoops = 0;
            while (_maxSpownCount > _spwancount)
            {
                if (_avoidInfiniteLoops > _maxSpownCount) {
                    _spwancount = _avoidInfiniteLoops;
                    break;
                }
                _avoidInfiniteLoops++;
                switch (Random.Range(0, 2))
                {
                    case 0:
                        _random = Random.Range(0, _rabbits.Length);
                        if (_rabbits[_random].gameObject.activeSelf == true)
                            continue;
                        _rabbits[_random].gameObject.SetActive(true);
                        break;
                    case 1:
                        _random = Random.Range(0, _masPeoples.Length);
                        if (_masPeoples[_random].gameObject.activeSelf == true)
                            continue;
                        _masPeoples[_random].gameObject.SetActive(true);
                        break;
                }
                yield return new WaitForSeconds(1);
                _spwancount++;
            }
            _patternAni.Play("EndSing");

        }

        public override void Play()
        {
            _patternAni.Play("Sing");
        }
    }
}