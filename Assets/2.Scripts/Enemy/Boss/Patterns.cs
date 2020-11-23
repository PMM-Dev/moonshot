using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    
    public abstract class Patterns : MonoBehaviour, IPattern
    {
        [Tooltip("발사체 프리펩 넣기")]
        [SerializeField]
        protected GameObject _projectile;
        [Tooltip("패턴 지속 시간")]
        [SerializeField]
        protected float _patternTime;
        [Tooltip("발사체 개수")]
        [SerializeField]
        protected float _projectileCount;
        [Tooltip("발사체 속도")]
        [SerializeField]
        protected float _projectileSpeed;
        protected int _spwoncount = 0;
        protected float _time = 0;

        protected GameObject _player;

        public abstract IEnumerator Run();
    }
}