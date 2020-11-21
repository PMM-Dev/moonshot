using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

namespace Player
{
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
        private float _gravityScale;
        [SerializeField]
        private Vector2 _gravity;
        [SerializeField]
        private float _normalJumpPower;
        [SerializeField]
        private float _wallJumpPower;
        [SerializeField]
        private float _stickGravity;
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
        private bool _isBeside;
        [SerializeField]
        private bool _isSlashing;
        [SerializeField]
        private bool _isSlashLocked;

        [SerializeField]
        private Vector2 _velocity;
        [SerializeField]
        private Vector2 _slashDirection;

        [SerializeField]
        private bool _isMoveInputLocked;
        [SerializeField]
        private bool _isJumpInputLocked;
        #endregion

        #region Event
        public Action SlashAction;
        public Action EndSlashAction;
        #endregion

        #region Reference
        private PlayerLogic _playerLogic;
        private PlayerSimulation _playerSimulation;
        private PlayerInput _playerInput;
        private PlayerCollisionTrigger _playerCollisionTrigger;
        private Animator _animator;
        private Transform _bodyTransform;
        private BoxCollider2D _boxCollider2D;
        [SerializeField]
        private Transform _slashRange;
        #endregion

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _bodyTransform = GetComponentInChildren<Animator>().transform;
            _playerCollisionTrigger = GetComponentInChildren<PlayerCollisionTrigger>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            _playerSimulation = new PlayerSimulation();
            _playerInput = new PlayerInput();
            _playerLogic = new PlayerLogic(this._playerSimulation, this._playerInput);

            InitializeEvent();
        }

        private void Update()
        {
            _moveDirection = _playerLogic.GetMoveDirection(_moveDirection, _playerLogic.GetMoveInput(), _stickDirection, _isGround, _isMoveInputLocked);
            _isAccel = _playerLogic.IsLookSameAsMove(_lookDirection, _moveDirection);

            if (_isGround)
            {
                _velocity.y = 0f;
                _isSlashLocked = false;
            }
            _jumpState = _playerLogic.GetJumpState(_isJumpInputLocked, _isGround, _moveDirection, _stickDirection);
            Jump();
            Gravity();
            Stick();
            Move();

            _playerInput.GetMouseDirection();
            _slashDirection = _playerInput.GetSlashDirection();
            Slash();

            CollideWithGround();
        }


        private void InitializeEvent()
        {
            SlashAction = delegate { };
            EndSlashAction = delegate { };

            _playerCollisionTrigger.CollisionTriggers[ColliderType.Left].OnTriggerEnter += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Left].OnTriggerStay += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Left].OnTriggerExit += CheckStick;

            _playerCollisionTrigger.CollisionTriggers[ColliderType.Right].OnTriggerEnter += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Right].OnTriggerStay += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Right].OnTriggerExit += CheckStick;


            _playerInput.InitializeEvent();
        }

        private void CheckStick(CollisionType collisionType, Collider2D collider2D, ColliderType colliderType)
        {
            if (collisionType == CollisionType.Exit)
            {
                _isBeside = false;
            }
            else if (collisionType == CollisionType.Stay)
            {
                _isBeside = true;
            }

            _stickDirection = _playerLogic.GetStickDirection(collisionType, collider2D, colliderType, _isGround, _moveDirection, _isSlashLocked);

            _animator.SetBool("isStick", _stickDirection != StickDirection.Idle ? true : false);
        }

        private void Move()
        {
            _currentSpeed = _playerSimulation.GetCurrentSpeed(_isAccel, _lookDirection, _currentSpeed, _speed, _acceleration, _deceleration, _isGround);

            _lookDirection = _playerSimulation.GetLookDirection(_lookDirection, _moveDirection, _currentSpeed, _stickDirection);
            _velocity.x = _playerSimulation.MovePosition(_lookDirection, _currentSpeed);

            if (!_isSlashing)
                transform.Translate(_velocity * Time.deltaTime);

            _animator.SetFloat("currentSpeed", _currentSpeed);
            _bodyTransform.localScale = new Vector3((int)_lookDirection * -1, 1);
        }

        private void Jump()
        {
            if (_jumpState == JumpState.None)
            {
                return ;
            }

            if (_jumpState == JumpState.Wall)
            {
                _lookDirection = (LookDirection)((int)_stickDirection * (-1));
                _velocity.y = _wallJumpPower;
                _isMoveInputLocked = true;
                StartCoroutine(ForceWallJumpTimer((int)(_lookDirection) * _speed * Time.deltaTime, 0.35f));
            }
            else
            {
                _velocity.y = _normalJumpPower;
            }
        }

        private void Stick()
        {
            if (_playerLogic.IsStickAvailable(_stickDirection, _isMoveInputLocked, _isSlashLocked))
            {
                _velocity.y = -_stickGravity ;
            }
        }

        private IEnumerator ForceWallJumpTimer(float xDir, float forceTime)
        {
            float time = 0f;
            while (time < forceTime && !_isGround)
            {
                yield return null;
                _currentSpeed = _speed;
                time += Time.deltaTime;
            }

            _isMoveInputLocked = false;
        }

        private void Slash()
        {
            if (_playerLogic.IsSlashAvailable(_playerInput.GetMouseButtonUp(), _isSlashLocked, _stickDirection))
            { 
                if (_playerInput.GetMouseInputDistance() > 2f)
                {
                    StartCoroutine(ForceSlash(_slashDirection, 0.125f));
                }
            }
        }

        private IEnumerator ForceSlash(Vector2 direction, float forceTime)
        {
            _isSlashLocked = true;
            SlashAction?.Invoke();

            float time = 0f;
            _isSlashing = true;
            _animator.SetBool("isSlash", _isSlashing);
            float angle = _playerInput.GetSlashAngle();

            if (_slashRange != null)
            {
                _slashRange.localScale = new Vector3(1f, 1f, 1f);
                _slashRange.position = transform.position;




                _slashRange.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle * -1));
            }

            Vector3 origin = _slashRange == null ? Vector3.zero : _slashRange.position;

            if (angle < 0)
            {
                _lookDirection = LookDirection.Left;
            }
            else
            {
                _lookDirection = LookDirection.Right;
            }

            while (time < forceTime && !_isBeside)
            {
                yield return null;
                _currentSpeed = 80f;
                _velocity = direction * _currentSpeed;
                transform.Translate(_velocity * Time.deltaTime);

                time += Time.deltaTime;
            }
            _isSlashing = false;
            _velocity = Vector2.zero;
            _animator.SetBool("isSlash", _isSlashing);

            Vector2 target = transform.position;


            if (_slashRange != null)
            {
                float distance = Vector2.Distance(origin, target);
                _slashRange.localScale = new Vector3(1f, distance, 1f);

            }

            EndSlashAction?.Invoke();
        }

        private void Gravity()
        {
            _velocity.y += -_gravityScale * Time.deltaTime;
        }

        private void CollideWithGround()
        {
            _isGround = false;

            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _boxCollider2D.size, 0);
            foreach (Collider2D hit in hits)
            {
                if (hit.Equals(_boxCollider2D) || hit.gameObject.layer != LayerMask.NameToLayer("Ground"))
                    continue;

                ColliderDistance2D colliderDistance = hit.Distance(_boxCollider2D);

                if (colliderDistance.isOverlapped)
                {
                    transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                    if (Vector2.Angle(colliderDistance.normal, Vector2.up) == 0 && _velocity.y < 0)
                    {
                        if (!_isSlashing)
                        {
                            _isGround = true;

                        }

                    }
                    else if (Vector2.Angle(colliderDistance.normal, Vector2.up) == 180 && _velocity.y > 0)
                    {
                        _velocity.y = 0f;
                    }
                }
            }
            _animator.SetBool("isGround", _isGround);
        }
    }
}
