using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class SingPattern : Patterns, IPattern
    {
        [Header("안보이는 몹 할당")]
        [SerializeField]
        GameObject[] _rabbitPostion;
        [SerializeField]
        GameObject[] _wolfPostion;
        [SerializeField]
        GameObject[] _masPeoplePostion;
        [Header("전체 몹값보다 충분히 낮게 잡지 않으면 겜 멈춤")]
        [SerializeField]
        int _maxSpownCount;

        int _random;
        private int _spwancount;
        private int _avoidInfiniteLoops;


        public override IEnumerator Run()
        {
            _spwancount = 0;
            _avoidInfiniteLoops = 0;
            while (_maxSpownCount > _spwancount)
            {
                if (_avoidInfiniteLoops > _maxSpownCount) {
                    _spwancount = _avoidInfiniteLoops;
                    break;
                }
                _avoidInfiniteLoops++;
                switch (Random.Range(0, 3))
                {
                    case 0:
                        _random = Random.Range(0, _rabbitPostion.Length);
                        if (_rabbitPostion[_random].activeSelf == true)
                            continue;
                        _rabbitPostion[_random].SetActive(true);
                        break;
                    case 1:
                        _random = Random.Range(0, _wolfPostion.Length);
                        if (_wolfPostion[_random].activeSelf == true)
                            continue;
                        _wolfPostion[_random].SetActive(true);
                        break;
                    case 2:
                        _random = Random.Range(0, _masPeoplePostion.Length);
                        if (_masPeoplePostion[_random].activeSelf == true)
                            continue;
                        _masPeoplePostion[_random].SetActive(true);
                        break;
                }
                yield return new WaitForSeconds(1);
                _spwancount++;
            }

        }
    }
}