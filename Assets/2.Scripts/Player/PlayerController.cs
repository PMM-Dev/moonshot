using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour
    {
        #region Setting
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _acceleration;
        [SerializeField]
        private float _deceleration;
        [SerializeField]
        private float _currentSpeed;
        [SerializeField]
        private float _jumpPower;
        #endregion

        #region State
        [SerializeField]
        private MoveDirection _moveDirection;
        [SerializeField]
        private LookDirection _lookDirection = LookDirection.Right;
        private bool _isAccel;
        [SerializeField]
        private bool _isGround;
        private bool _isClimb;
        #endregion

        #region Reference
        private PlayerLogic _playerLogic;
        private PlayerSimulation _playerSimulation;
        private PlayerInput _playerInput;
        private PlayerCollisionTrigger _playerCollisionTrigger;
        private Rigidbody2D _rigidbody2D;
        #endregion

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerCollisionTrigger = GetComponentInChildren<PlayerCollisionTrigger>();
        }

        private void Start()
        {
            _playerSimulation = new PlayerSimulation();
            _playerInput = new PlayerInput();
            _playerLogic = new PlayerLogic(this._playerSimulation, this._playerInput);

            _playerCollisionTrigger.CollisionTriggers[ColliderType.Bottom].OnTriggerEnter += CheckGrounded;

            _playerCollisionTrigger.CollisionTriggers[ColliderType.Bottom].OnTriggerExit += CheckGrounded;
        }

        private void Update()
        {
            _moveDirection = _playerLogic.GetMoveInput();
            _isAccel = _playerLogic.IsLookSameAsMove(_lookDirection, _moveDirection);

        }
        private void FixedUpdate()
        {
            Move();
            if (_playerLogic.IsInput(PressType.Stay, InputType.Jump))
            {
                Jump();
            }
        }

        private void CheckGrounded(CollisionType collisionType, Collider2D collider2D)
        {
            _isGround = _playerLogic.IsGround(collisionType, collider2D);
        }

        private void Move()
        {
            _currentSpeed = _playerSimulation.GetCurrentSpeed(_isAccel, _lookDirection, _currentSpeed, _speed, _acceleration, _deceleration);
            _lookDirection = _playerSimulation.GetLookDirection(_lookDirection, _moveDirection, _currentSpeed);
            transform.position = _playerSimulation.MovePosition(transform.position, _lookDirection, _currentSpeed);
        }

        private void Jump()
        {
            if (!_isGround)
                return;

            _rigidbody2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
}
