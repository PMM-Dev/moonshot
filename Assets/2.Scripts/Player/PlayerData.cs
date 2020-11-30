using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [Header("Move")]
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

        [Header("Jump")]
        [Range(1f, 100f)]
        [SerializeField]
        private float _normalJumpPower = 25f;
        public float NormalJumpPower => _normalJumpPower;
        [Range(1f, 100f)]
        [SerializeField]
        private float _wallJumpPower = 30f;
        public float WallJumpPower => _wallJumpPower;
        [Range(0.01f, 1f)]
        [SerializeField]
        private float _wallJumpKnockback = 0.25f;
        public float WallJumpKnockback => _wallJumpKnockback;
        [Range(1f, 3f)]
        [SerializeField]
        private float _wallJumpBonusSpeed= 1.5f;
        public float WallJumpBonusSpeed => _wallJumpBonusSpeed;

        [Header("Stick")]
        [Range(0f, 10f)]
        [SerializeField]
        private float _stickGravity = 0.8f;
        public float StickGravity => _stickGravity;

        [Header("Slash")]
        [Range(0.01f, 0.5f)]
        [SerializeField]
        private float _slashForceTime = 0.1f;
        public float SlashForceTime => _slashForceTime;
        [Range(1f, 200f)]
        [SerializeField]
        private float _slashSpeed = 80f;
        public float SlashSpeed => _slashSpeed;
        [Range(0.01f, 5f)]
        [SerializeField]
        private float _readyToSlashTimeLimit = 1f;
        public float ReadyToSlashTimeLimit => _readyToSlashTimeLimit;
        [Range(0.1f, 5f)]
        [SerializeField]
        private float _slashRangeDetection = 3f;
        public float SlashRangeDetection => _slashRangeDetection;
        [SerializeField]
        private Sprite _slashRangeSprite;
        public Sprite SlashRangeSprite => _slashRangeSprite;

        [Header("FX")]
        [Range(1f, 10f)]
        [SerializeField]
        private float _slashZoomSize = 5f;
        public float SlashZoomSize => _slashZoomSize;
        [Range(0.01f, 5f)]
        [SerializeField]
        private float _bulletTimeLimit = 0.5f;
        public float BulletTimeLimit => _bulletTimeLimit;
        [Range(0.01f, 1f)]
        [SerializeField]
        private float _bulletTimeSpeed = 0.05f;
        public float BulletTimeSpeed => _bulletTimeSpeed;
        [Range(0.01f, 100f)]
        [SerializeField]
        private float _bulletTimeDecreaseSpeed = 5f;
        public float BulletTimeDecreaseSpeed => _bulletTimeDecreaseSpeed;
        [Range(0.01f, 100f)]
        [SerializeField]
        private float _bulletTimeIncreaseSpeed = 30f;
        public float BulletTimeIncreaseSpeed => _bulletTimeIncreaseSpeed;
        
        [Header("Physics")]       
        [Range(0f, 200f)]
        [SerializeField]
        private float _gravityScale = 70f;
        public float GravityScale => _gravityScale;
        [Range(-50f, -5f)]
        [SerializeField]
        private float _gravityLimit = -30f;
        public float GravityLimit => _gravityLimit;
    }
}
