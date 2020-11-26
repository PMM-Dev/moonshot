using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{

    public abstract class Patterns : MonoBehaviour, IPattern ,IAnimation
    {
        [Tooltip("발사체 프리펩 넣기")]
        [SerializeField]
        protected GameObject _projectile;
        [Tooltip("발사체 속도")]
        [SerializeField]
        protected float _projectileSpeed;
        protected GameObject _player;
        protected Animator _patternAni;
        public GameObject Player
        {
            set
            {
                _player = value;
            }
        }
        public Animator PatternAni
        {
            set
            {
                _patternAni = value;
            }
        }

        public abstract void Play();

        public abstract IEnumerator Run();
    }
}