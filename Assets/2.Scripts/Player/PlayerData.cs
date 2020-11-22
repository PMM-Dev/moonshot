using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [Range(1f, 50f)]
        [SerializeField]
        private float _speed = 10f;
        public float Speed => _speed;
        [Range(0.1f, 50f)]
        [SerializeField]
        private float _acceleration = 1f;
        public float Acceleration => _acceleration;
        [Range(0.1f, 50f)]
        [SerializeField]
        private float _deceleration = 2f;
        public float Deceleration => _deceleration;
        [Range(1f, 200f)]
        [SerializeField]
        private float _gravityScale = 70f;
        public float GravityScale => _gravityScale;
        [Range(1f, 100f)]
        [SerializeField]
        private float _normalJumpPower = 25f;
        public float NormalJumpPower => _normalJumpPower;
        [Range(1f, 100f)]
        [SerializeField]
        private float _wallJumpPower = 30f;
        public float WallJumpPower => _wallJumpPower;
        [Range(0.1f, 10f)]
        [SerializeField]
        private float _stickGravity = 0.8f;
        public float StickGravity => _stickGravity;
        [Range(-50f, -5f)]
        [SerializeField]
        private float _gravityLimit = -30f;
        public float GravityLimit => _gravityLimit;
        [Range(0.01f, 0.5f)]
        [SerializeField]
        private float _slashDistance = 0.1f;
        public float SlashDistance => _slashDistance;
        [Range(0.01f, 5f)]
        [SerializeField]
        private float _bulletTimeLimit = 1f;
        public float BulletTimeLimit => _bulletTimeLimit;
        [Range(0.01f, 1f)]
        [SerializeField]
        private float _bulletTimeSpeed = 0.05f;
        public float BulletTimeSpeed => _bulletTimeSpeed;
    }
}
