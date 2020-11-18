﻿using System.Collections;
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
        private float _normalJumpPower;
        [SerializeField]
        private float _wallJumpPower;
        [SerializeField]
        private float _stickPower;
        #endregion

        #region State
        [SerializeField]
        private MoveDirection _moveDirection;
        [SerializeField]
        private LookDirection _lookDirection = LookDirection.Right;
        [SerializeField]
        private StickDirection _stickDirection;
        [SerializeField]
        private JumpState _jumpState;

        private bool _isAccel;
        [SerializeField]
        private bool _isGround;
        [SerializeField]
        private bool _isClimb;

        [SerializeField]
        private bool _isMoveInputLocked;
        #endregion

        #region Reference
        private PlayerLogic _playerLogic;
        private PlayerSimulation _playerSimulation;
        private PlayerInput _playerInput;
        private PlayerCollisionTrigger _playerCollisionTrigger;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private Transform _bodyTransform;
        #endregion

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            _playerCollisionTrigger = GetComponentInChildren<PlayerCollisionTrigger>();
            _bodyTransform = GetComponentInChildren<Animator>().transform;
        }

        private void Start()
        {
            _playerSimulation = new PlayerSimulation();
            _playerInput = new PlayerInput();
            _playerLogic = new PlayerLogic(this._playerSimulation, this._playerInput);

            _playerCollisionTrigger.CollisionTriggers[ColliderType.Bottom].OnTriggerEnter += CheckGrond;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Bottom].OnTriggerExit += CheckGrond;

            _playerCollisionTrigger.CollisionTriggers[ColliderType.Left].OnTriggerEnter += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Left].OnTriggerStay += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Left].OnTriggerExit += CheckStick;

            _playerCollisionTrigger.CollisionTriggers[ColliderType.Right].OnTriggerEnter += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Right].OnTriggerStay += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Right].OnTriggerExit += CheckStick;
        }

        private void Update()
        {


        }

        private void FixedUpdate()
        {
            _moveDirection = _playerLogic.GetMoveDirection(_moveDirection, _playerLogic.GetMoveInput(), _stickDirection, _isGround, _isMoveInputLocked);
            _isAccel = _playerLogic.IsLookSameAsMove(_lookDirection, _moveDirection);

            Move();
            Jump();
            Stick();
        }

        private void CheckGrond(CollisionType collisionType, Collider2D collider2D, ColliderType colliderType)
        {
            _isGround = _playerLogic.IsGround(collisionType, collider2D);
            _animator.SetBool("isGround", _isGround);
        }

        private void CheckStick(CollisionType collisionType, Collider2D collider2D, ColliderType colliderType)
        {
            _stickDirection = _playerLogic.GetStickDirection(collisionType, collider2D, colliderType, _isGround, _moveDirection);
            _currentSpeed = _stickDirection != StickDirection.Idle ? 0 : _currentSpeed;
            _animator.SetBool("isStick", _stickDirection != StickDirection.Idle ? true : false);
        }

        private void Move()
        {
            _currentSpeed = _playerSimulation.GetCurrentSpeed(_isAccel, _lookDirection, _currentSpeed, _speed, _acceleration, _deceleration, _isGround);

            if (_stickDirection != StickDirection.Idle)
                _currentSpeed = 0f;

            _lookDirection = _playerSimulation.GetLookDirection(_lookDirection, _moveDirection, _currentSpeed, _stickDirection);
            transform.position = _playerSimulation.MovePosition(transform.position, _lookDirection, _currentSpeed);

            _animator.SetFloat("currentSpeed", _currentSpeed);
            _bodyTransform.localScale = new Vector3((int)_lookDirection * -1, 1);
        }

        private void Jump()
        {
            _jumpState = _playerLogic.GetJumpState(_isGround, _moveDirection, _stickDirection);
            if (_jumpState == JumpState.None)
            {
                return;
            }
            
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);
            Vector2 jumpDirection = _playerLogic.GetJumpDiretion(_jumpState, _stickDirection);
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);

            if (_jumpState == JumpState.Wall)
            {
                _lookDirection = (LookDirection)((int)_stickDirection * (-1));
                _rigidbody2D.AddForce(_playerSimulation.Jump(jumpDirection, _wallJumpPower), ForceMode2D.Impulse);
                _isMoveInputLocked = true;
                StartCoroutine(ForceWallJumpTimer(1f));
            }
            else
            {
                _rigidbody2D.AddForce(_playerSimulation.Jump(jumpDirection, _normalJumpPower), ForceMode2D.Impulse);
            }
        }

        private void Stick()
        {
            if (_stickDirection != StickDirection.Idle)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -_stickPower * 10f * Time.fixedDeltaTime);
            }
        }

        private IEnumerator ForceWallJumpTimer(float forceTime)
        {
            float time = 0f;
            while (time < forceTime && !_isGround)
            {
                time += Time.deltaTime;
                yield return null;
            }
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
            _isMoveInputLocked = false;
        }
    }
}