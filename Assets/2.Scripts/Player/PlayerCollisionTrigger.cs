using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCollisionTrigger : MonoBehaviour
    {
        private Dictionary<ColliderType, CollisionTrigger> _collisionTriggers;
        public Dictionary<ColliderType, CollisionTrigger> CollisionTriggers
        {
            get { return _collisionTriggers; }
        }

        private void Awake()
        {
            InitializeCollisionTrigger();
        }

        public void InitializeCollisionTrigger()
        {
            _collisionTriggers = new Dictionary<ColliderType, CollisionTrigger>();
            for (int i = 0; i < transform.childCount; i++)
            {
                CollisionTrigger collisionTrigger = transform.GetChild(i).GetComponent<CollisionTrigger>();
                _collisionTriggers.Add(collisionTrigger.ColliderType, collisionTrigger);
            }
        }
    }
}

