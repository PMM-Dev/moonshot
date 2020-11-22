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
        private float _speed;
        public float Speed => _speed;
        [Range(0.1f, 50f)]
        [SerializeField]
        private float _acceleration;
        public float Acceleration => _acceleration;
        [Range(0.1f, 50f)]
        [SerializeField]
        private float _deceleration;
        public float Deceleration => _deceleration;
        [Range(1f, 200f)]
        [SerializeField]
        private float _gravityScale;
        public float GravityScale => _gravityScale;
        [Range(1f, 100f)]
        [SerializeField]
        private float _normalJumpPower;
        public float NormalJumpPower => _normalJumpPower;
        [Range(1f, 100f)]
        [SerializeField]
        private float _wallJumpPower;
        public float WallJumpPower => _wallJumpPower;
        [Range(0.1f, 10f)]
        [SerializeField]
        private float _stickGravity;
        public float StickGravity => _stickGravity;
        [Range(-50f, -5f)]
        [SerializeField]
        private float _gravityLimit;
        public float GravityLimit => _gravityLimit;
        [Range(0.01f, 0.5f)]
        [SerializeField]
        private float _slashDistance;
        public float SlashDistance => _slashDistance;
    }
}
