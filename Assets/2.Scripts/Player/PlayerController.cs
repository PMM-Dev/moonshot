using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
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
        [SerializeField]
        private StickDirection _stickDirection;
        private bool _isAccel;
        [SerializeField]
        private bool _isGround;
        [SerializeField]
        private bool _isClimb;

        #endregion

        #region Reference
        private PlayerLogic _playerLogic;
        private PlayerSimulation _playerSimulation;
        private PlayerInput _playerInput;
        private PlayerCollisionTrigger _playerCollisionTrigger;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        #endregion

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            _playerCollisionTrigger = GetComponentInChildren<PlayerCollisionTrigger>();
        }

        private void Start()
        {
            _playerSimulation = new PlayerSimulation();
            _playerInput = new PlayerInput();
            _playerLogic = new PlayerLogic(this._playerSimulation, this._playerInput);

            _playerCollisionTrigger.CollisionTriggers[ColliderType.Bottom].OnTriggerEnter += CheckGrond;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Bottom].OnTriggerExit += CheckGrond;
        }

        private void Update()
        {
            _moveDirection = _playerLogic.GetMoveInput();
            _isAccel = _playerLogic.IsLookSameAsMove(_lookDirection, _moveDirection);
        }
        private void FixedUpdate()
        {
            Move();

            if (_playerLogic.IsJumpAvailable(_isGround))
            {
                Jump();
            }
        }

        private void CheckGrond(CollisionType collisionType, Collider2D collider2D)
        {
            _isGround = _playerLogic.IsGround(collisionType, collider2D);
        }

        private void CheckClimb(CollisionType collisionType, Collider2D collider2D)
        {
            _isClimb = _playerLogic.IsGround(collisionType, collider2D);
        }

        private void Move()
        {
            _currentSpeed = _playerSimulation.GetCurrentSpeed(_isAccel, _lookDirection, _currentSpeed, _speed, _acceleration, _deceleration);
            _animator.SetFloat("currentSpeed", _currentSpeed);
            _lookDirection = _playerSimulation.GetLookDirection(_lookDirection, _moveDirection, _currentSpeed);
            transform.localScale = new Vector3((int)_lookDirection * -1, 1);
            transform.position = _playerSimulation.MovePosition(transform.position, _lookDirection, _currentSpeed);
        }

        private void Jump()
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }

        private void Flip()
        {

        }

        private void Climb()
        {
            
        }
    }
}
