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

        private BoxCollider2D _boxCollider2D;
        public BoxCollider2D BoxCollider2D
        {
            get { return _boxCollider2D; }
        }

        private Action<CollisionType, Collider2D, ColliderType> _onTriggerEnter;
        public Action<CollisionType, Collider2D, ColliderType> OnTriggerEnter
        {
            get { return _onTriggerEnter; }
            set { _onTriggerEnter = value; }
        }

        private Action<CollisionType, Collider2D, ColliderType> _onTriggerExit;
        public Action<CollisionType, Collider2D, ColliderType> OnTriggerExit
        {
            get { return _onTriggerExit; }
            set { _onTriggerExit = value; }
        }

        private Action<CollisionType, Collider2D, ColliderType> _onTriggerStay;
        public Action<CollisionType, Collider2D, ColliderType> OnTriggerStay
        {
            get { return _onTriggerStay; }
            set { _onTriggerStay = value; }
        }

        private void Awake()
        {
            _onTriggerEnter = delegate { };
            _onTriggerExit = delegate { };
            _onTriggerStay = delegate { };
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _onTriggerEnter?.Invoke(CollisionType.Enter, collision, _colliderType);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            _onTriggerStay?.Invoke(CollisionType.Stay, collision, _colliderType);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _onTriggerExit?.Invoke(CollisionType.Exit, collision, _colliderType);
        }
    }
}
