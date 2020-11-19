using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
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
        private float _gravityScale;
        [SerializeField]
        private Vector2 _gravity;
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
        private bool _isBeside;
        [SerializeField]
        private Vector2 _velocity;

        [SerializeField]
        private bool _isMoveInputLocked;
        [SerializeField]
        private bool _isJumpInputLocked;
        #endregion

        #region Reference
        private PlayerLogic _playerLogic;
        private PlayerSimulation _playerSimulation;
        private PlayerInput _playerInput;
        private PlayerCollisionTrigger _playerCollisionTrigger;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private Transform _bodyTransform;
        private List<Collider2D> _hits;
        private BoxCollider2D _boxCollider2D;
        private CircleCollider2D _circleCollider2D;
        #endregion

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            _bodyTransform = GetComponentInChildren<Animator>().transform;
            _playerCollisionTrigger = GetComponentInChildren<PlayerCollisionTrigger>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
        }

        private void Start()
        {
            _playerSimulation = new PlayerSimulation();
            _playerInput = new PlayerInput();
            _playerLogic = new PlayerLogic(this._playerSimulation, this._playerInput);

            
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Bottom].OnTriggerEnter += CheckGrond;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Bottom].OnTriggerExit += CheckGrond;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Bottom].OnTriggerStay += CheckGrond;
            

            _playerCollisionTrigger.CollisionTriggers[ColliderType.Left].OnTriggerEnter += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Left].OnTriggerStay += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Left].OnTriggerExit += CheckStick;

            _playerCollisionTrigger.CollisionTriggers[ColliderType.Right].OnTriggerEnter += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Right].OnTriggerStay += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Right].OnTriggerExit += CheckStick;
        }

        private void Update()
        {       
            _jumpState = _playerLogic.GetJumpState(_isJumpInputLocked, _isGround, _moveDirection, _stickDirection);
            _moveDirection = _playerLogic.GetMoveDirection(_moveDirection, _playerLogic.GetMoveInput(), _stickDirection, _isGround, _isMoveInputLocked);
            _isAccel = _playerLogic.IsLookSameAsMove(_lookDirection, _moveDirection);
            /*
            if (_isGround)
            {
                _velocity.y = 0f;
            }
            */
            Stick();
            Move();
            Gravity();
            Jump();
            CollideWithGround();
            _animator.SetBool("isGround", _isGround);
        }

        private void FixedUpdate()
        {

        }

        private void CheckGrond(CollisionType collisionType, Collider2D collider2D, ColliderType colliderType)
        {
            _isGround = _playerLogic.IsGround(collisionType, collider2D);
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
            _velocity.x = _playerSimulation.MovePosition(_lookDirection, _currentSpeed);

            transform.Translate(_velocity);

            _animator.SetFloat("currentSpeed", _currentSpeed);
            _bodyTransform.localScale = new Vector3((int)_lookDirection * -1, 1);
        }

        private void Jump()
        {
            if (_jumpState == JumpState.None)
            {
                return;
            }
 
            Vector2 jumpDirection = _playerLogic.GetJumpDiretion(_jumpState, _stickDirection);
            _velocity.y = 0f;

            if (_jumpState == JumpState.Wall)
            {
                _lookDirection = (LookDirection)((int)_stickDirection * (-1));
                _velocity.y += _wallJumpPower * Time.deltaTime;
                _isMoveInputLocked = true;
                StartCoroutine(ForceWallJumpTimer((int)(_lookDirection) * _speed * Time.deltaTime, 0.25f));
            }
            else
            {
                _velocity.y = _normalJumpPower * Time.deltaTime;
                Debug.Log("JU");
            }
        }

        private void Stick()
        {
            if (_stickDirection != StickDirection.Idle && !_isMoveInputLocked)
            {
                _velocity.y = -_stickPower ;
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

        private IEnumerator ForceJumpTimer(float forceTime)
        {
            float time = 0f;
            while (time < forceTime)
            {
                yield return null;
                time += Time.deltaTime;
            }
            _isJumpInputLocked = false;
        }

        private void Gravity()
        {
            if (_isGround)
            {
                _velocity = Vector2.zero;
            }
            else
            {
                _gravity = new Vector2(0, -_gravityScale);
                _velocity += _gravity * Time.deltaTime;
            }

        }

        private void CollideWithGround()
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _boxCollider2D.size, 0);
            foreach (Collider2D hit in hits)
            {
                if (hit.Equals(_boxCollider2D) || hit.gameObject.layer != LayerMask.NameToLayer("Ground"))
                    continue;

                ColliderDistance2D colliderDistance = hit.Distance(_boxCollider2D);

                if (colliderDistance.isOverlapped)
                {
                    transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                }
            }

            _isGround = false;

            Collider2D[] groundHits = Physics2D.OverlapCircleAll(transform.position, _circleCollider2D.radius);
            foreach (Collider2D hit in groundHits)
            {
                if (groundHits.Equals(_circleCollider2D) || hit.gameObject.layer != LayerMask.NameToLayer("Ground"))
                    continue;
                ColliderDistance2D colliderDistance = hit.Distance(_circleCollider2D);

                if (colliderDistance.isOverlapped)
                {
                    _isGround = true;
                }
            }
        }
    }
}
