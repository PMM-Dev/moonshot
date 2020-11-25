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
        [Tooltip("발사체 속도")]
        [SerializeField]
        protected float _projectileSpeed;
        protected GameObject _player;

        public GameObject Player {
            set {
                _player = value;
            }
        }

        public abstract IEnumerator Run();
    }
}