using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CollisionTrigger : MonoBehaviour
    {
        [SerializeField]
        private ColliderType _colliderType;
        public ColliderType ColliderType
        {
            get { return _colliderType; }
            set { _colliderType = value; }
        }

        private Action<CollisionType, Collider2D> _onTriggerEnter;
        public Action<CollisionType, Collider2D> OnTriggerEnter
        {
            get { return _onTriggerEnter; }
            set { _onTriggerEnter = value; }
        }

        private Action<CollisionType, Collider2D> _onTriggerExit;
        public Action<CollisionType, Collider2D> OnTriggerExit
        {
            get { return _onTriggerExit; }
            set { _onTriggerExit = value; }
        }

        private Action<CollisionType, Collider2D> _onTriggerStay;
        public Action<CollisionType, Collider2D> OnTriggerStay
        {
            get { return _onTriggerStay; }
            set { _onTriggerStay = value; }
        }

        private void Awake()
        {
            _onTriggerEnter = delegate { };
            _onTriggerExit = delegate { };
            _onTriggerStay = delegate { };
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _onTriggerEnter?.Invoke(CollisionType.Enter, collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            _onTriggerStay?.Invoke(CollisionType.Stay, collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _onTriggerExit?.Invoke(CollisionType.Exit, collision);
        }
    }
}
