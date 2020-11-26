using JetBrains.Annotations;
using Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour, IDamage
    {
        #region Reference
        [Header("Reference")]
        [SerializeField]
        private PlayerData _data;
        private GameObject _slashRange;
        [SerializeField]
        private GameObject _slashArrow;
        private PlayerLogic _playerLogic;
        private PlayerSimulation _playerSimulation;
        private PlayerInput _playerInput;
        private PlayerCollisionTrigger _playerCollisionTrigger;
        private Animator _animator;
        private Transform _bodyTransform;
        private BoxCollider2D _boxCollider2D;
        private PlayerFX _playerFX;
        #endregion

        #region State
        [Header("Value State")]
        [SerializeField]
        private float _currentSpeed;
        [Header("Type state")]
        [SerializeField]
        private MoveDirection _moveDirection;
        [SerializeField]
        private LookDirection _lookDirection = LookDirection.Right;
        [SerializeField]
        private StickDirection _stickDirection;
        [SerializeField]
        private JumpState _jumpState;
        [SerializeField]
        private MoveDirection _besideDirection;

        private bool _isAccel;
        [Header("Bool state")]
        [SerializeField]
        private bool _isTestMode;
        [SerializeField]
        private bool _isGodMode;
        [SerializeField]
        private bool _isGround;
        [SerializeField]
        private bool _isSlashing;
        [SerializeField]
        private bool _isSlashLocked;
        [SerializeField]
        private bool _isMoveInputLocked;
        [SerializeField]
        private bool _isJumpLocked;
        [SerializeField]
        private bool _isBulletTime;
        [Header("Vector state")]
        [SerializeField]
        private Vector2 _velocity;
        [SerializeField]
        private Vector2 _slashDirection;
        private bool _isSlashAvailable;

        #endregion

        #region Event
        public Action<LookDirection, Vector3> SlashAction;
        public Action EndSlashAction;
        public Action SuccessSlashAction;
        public Action FailedSlashAction;
        public Action NormalJumpAction;
        public Action WallJumpAction;
        public Action<LookDirection> StickAction;
        public Action StopStickAction;
        #endregion

        private Coroutine _bulletTimeCoroutine;
        private SoundHelper _soundHelper;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _bodyTransform = GetComponentInChildren<Animator>().transform;
            _playerCollisionTrigger = GetComponentInChildren<PlayerCollisionTrigger>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _playerFX = GetComponent<PlayerFX>();

            _soundHelper = gameObject.AddComponent<SoundHelper>();
        }

        private void Start()
        {
            _playerSimulation = new PlayerSimulation();
            _playerInput = new PlayerInput();
            _playerLogic = new PlayerLogic(this._playerSimulation, this._playerInput);
            _slashRange = SlashRange.Instance.transform.parent.gameObject;
            _slashRange.SetActive(false);
            _slashArrow.gameObject.SetActive(false);
            SlashRange.Instance.PlayerController = this;
            _slashArrow.SetActive(false);
            InitializeEvent();
        }

        private void Update()
        {
            if (!_isSlashing)
            {
                _moveDirection = _playerLogic.GetMoveDirection(_moveDirection, _playerLogic.GetMoveInput(), _stickDirection, _isGround, _isMoveInputLocked);
            }

            if (_isGround)
            {
                _velocity.y = 0f;
                if (!_isSlashing)
                    _isSlashLocked = false;
            }

            _isAccel = _playerLogic.IsLookSameAsMove(_lookDirection, _moveDirection);
            _jumpState = _playerLogic.GetJumpState(_isJumpLocked, _isGround, _moveDirection, _stickDirection);
            Jump();
            Gravity();
            Stick();
            Move();
            Slash();
            CollideWithGround();
        }


        private void InitializeEvent()
        {
            SlashAction = delegate { };
            EndSlashAction = delegate { };
            SuccessSlashAction = delegate { };
            FailedSlashAction = delegate { };
            NormalJumpAction = delegate { };
            WallJumpAction = delegate { };
            StopStickAction = delegate { };
            StickAction = delegate { };

            SuccessSlashAction += SuccessSlashEvent;

            _playerCollisionTrigger.CollisionTriggers[ColliderType.Left].OnTriggerEnter += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Left].OnTriggerStay += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Left].OnTriggerExit += CheckStick;

            _playerCollisionTrigger.CollisionTriggers[ColliderType.Right].OnTriggerEnter += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Right].OnTriggerStay += CheckStick;
            _playerCollisionTrigger.CollisionTriggers[ColliderType.Right].OnTriggerExit += CheckStick;

            _playerInput.InitializeEvent();

            _playerFX.InitializeEvent(this);
        }

        private void CheckStick(CollisionType collisionType, Collider2D collider2D, ColliderType colliderType)
        {
            if (collider2D.gameObject.CompareTag("Ground"))
            {
                if (collisionType == CollisionType.Exit)
                {
                    _besideDirection = MoveDirection.Idle;
                }
                else if (collisionType == CollisionType.Stay)
                {
                    _besideDirection = (MoveDirection)((int)colliderType);
                }

                _stickDirection = _playerLogic.GetStickDirection(collisionType, collider2D, colliderType, _isGround, _moveDirection, _isSlashLocked);

                _animator.SetBool("isStick", _stickDirection != StickDirection.Idle ? true : false);
            }
        }

        private void Move()
        {
            _currentSpeed = _playerSimulation.GetCurrentSpeed(_isAccel, _lookDirection, _currentSpeed, _data.Speed, _data.Acceleration, _data.Deceleration, _isGround);

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
                return;
            }

            _isJumpLocked = true;

            if (_jumpState == JumpState.Wall)
            {
                _lookDirection = (LookDirection)((int)_stickDirection * (-1));
                _velocity.y = _data.WallJumpPower;
                _isMoveInputLocked = true;
                StartCoroutine(ForceWallJumpTimer((int)(_lookDirection) * _data.Speed * Time.deltaTime, 0.35f));
                WallJumpAction?.Invoke();
            }
            else
            {
                NormalJumpAction?.Invoke();
                _velocity.y = _data.NormalJumpPower;
            }
        }

        private void Stick()
        {
            if (_playerLogic.IsStickAvailable(_stickDirection, _isMoveInputLocked, _isSlashLocked, _isBulletTime))
            {
                _isJumpLocked = false;
                _velocity.y = -_data.StickGravity;
                StickAction?.Invoke(_lookDirection);
            }
            else
            {
                StopStickAction?.Invoke();
            }
        }

        private IEnumerator ForceWallJumpTimer(float xDir, float forceTime)
        {
            float time = 0f;
            while (time < forceTime && !_isGround)
            {
                yield return null;
                _currentSpeed = _data.Speed;
                time += Time.deltaTime;
            }

            _isMoveInputLocked = false;
        }

        private void Slash()
        {
            if (_playerLogic.IsSlashAvailable(_isSlashLocked, _stickDirection) && _playerInput.GetMouseButtonDown(0) && !_isBulletTime)
            {
                _isBulletTime = true;
                if (_bulletTimeCoroutine != null)
                {
                    StopCoroutine(_bulletTimeCoroutine);
                }
                _bulletTimeCoroutine = StartCoroutine(ReadyToSlash(_data.BulletTimeDecreaseSpeed, _data.BulletTimeIncreaseSpeed, _data.BulletTimeSpeed));
            }
        }

        private IEnumerator ForceSlash(Vector2 direction, float forceTime)
        {
            _isJumpLocked = true;
            _isSlashing = true;
            _slashRange.SetActive(true);

            float angle = _playerInput.GetSlashAngle();
            Vector3 rotateValue = new Vector3(0f, 0f, angle * -1);

            _slashRange.transform.localScale = new Vector3(1f, 1f, 1f);
            _slashRange.transform.position = transform.position;

            _slashRange.transform.rotation = Quaternion.Euler(rotateValue);

            Vector3 origin = transform.position;

            if (angle < 0)
            {
                _lookDirection = LookDirection.Left;
            }
            else
            {
                _lookDirection = LookDirection.Right;
            }

            _animator.SetBool("isSlash", _isSlashing);
            _soundHelper.PlaySound("Slash");
            SlashAction?.Invoke(_lookDirection, rotateValue);

            Vector2 target = transform.position;
            float distance = Vector2.Distance(origin, target);

            _isSlashLocked = true;

            float time = 0f;
            while (time < forceTime)
            {
                if ((int)_besideDirection == (int)_lookDirection)
                    break;

                _currentSpeed = 80f;
                _velocity = direction * _currentSpeed;
                transform.Translate(_velocity * Time.deltaTime);

                target = transform.position;
                distance = Vector2.Distance(origin, target);
                _slashRange.transform.localScale = new Vector3(1f, distance == 0f ? 1f : distance, 1f / _data.SlashRangeDetection) * _data.SlashRangeDetection;

                time += Time.deltaTime;
                yield return null;
            }

            _isSlashing = false;
            _velocity = Vector2.zero;
            _animator.SetBool("isSlash", _isSlashing);

            target = transform.position;
            distance = Vector2.Distance(origin, target);
            _slashRange.transform.localScale = new Vector3(1f, distance == 0f ? 1f : distance, 1f / _data.SlashRangeDetection) * _data.SlashRangeDetection;

            time = 0f;
            while (time < 0.1f)
            {
                time += Time.deltaTime;
                yield return null;
            }

            _slashRange.gameObject.SetActive(false);

            EndSlashAction?.Invoke();
        }

        private void Gravity()
        {
            if (_velocity.y > _data.GravityLimit)
                _velocity.y += -_data.GravityScale * Time.deltaTime;
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
                            _isJumpLocked = false;
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

        private void SuccessSlashEvent()
        {
            _isSlashLocked = false;
            _isJumpLocked = false;
            _bulletTimeCoroutine = StartCoroutine(BulletTime(_data.BulletTimeSpeed + 0.15f, _data.BulletTimeDecreaseSpeed, _data.BulletTimeIncreaseSpeed, _data.BulletTimeSpeed));
        }

        private IEnumerator BulletTime(float currentTime, float decreaseSpeed, float increaseSpeed, float minSpeed)
        {
            float time = 0f;
            float progress = 0f;
            float currentTimeScale = currentTime;

            while (progress < 1f)
            {
                time += Time.deltaTime / Time.timeScale;
                progress += Time.deltaTime * decreaseSpeed;
                Time.timeScale = Mathf.Lerp(currentTimeScale, minSpeed, progress);

                if (time > _data.BulletTimeLimit)
                {
                    break;
                }
                yield return null;
            }

            progress = 0f;
            currentTimeScale = Time.timeScale;
            while (progress < 1f)
            {
                Time.timeScale = Mathf.Lerp(currentTimeScale, 1f, progress);
                progress += Time.deltaTime * increaseSpeed;
                yield return null;
            }

            Time.timeScale = 1f;
        }

        public bool GetDamage()
        {
            if (_isSlashing || _isGodMode)
            {
                return false;
            }
            else
            {
                _isGodMode = true;
                _playerInput.PauseGameEvent();
                _animator.SetTrigger("trgDie");
                _animator.SetBool("isDie", true);

                if (!_isTestMode)
                {
                    StartCoroutine(DieEvent());
                }
                return true;
            }
        }

        private IEnumerator DieEvent()
        {
            float time = 0f;
            while (time < 2.5f)
            {
                time += Time.deltaTime;
                yield return null;
            }
            MainEventManager.Instance.GameoverEvent();
        }

        private IEnumerator ReadyToSlash(float decreaseSpeed, float increaseSpeed, float minSpeed)
        {
            _playerInput.GetOriginDirection(transform.position + new Vector3(0f, 0.5f, 0f));

            float time = 0f;
            float progress = 0f;
            float currentTimeScale = Time.timeScale;

            _slashArrow.SetActive(true);

            while (true)
            {
                time += Time.deltaTime / Time.timeScale;
                float angle = _playerInput.GetSlashAngle();
                _playerInput.GetTargetDirection();
                _slashArrow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (angle - 90) * -1));
                if (_playerInput.GetMouseButtonUp(0) || time > _data.ReadyToSlashTimeLimit)
                {
                    break;
                }
                if (progress < 1f)
                {
                    progress += Time.deltaTime * decreaseSpeed;
                    Time.timeScale = Mathf.Lerp(currentTimeScale, minSpeed, progress);
                }
                yield return null;
            }

            _slashArrow.SetActive(false);

            _slashDirection = _playerInput.GetSlashDirection();
            StartCoroutine(ForceSlash(_slashDirection, _data.SlashDistance));

            progress = 0f;
            Time.timeScale = minSpeed;
            currentTimeScale = Time.timeScale;
            while (progress < 1f)
            {
                Time.timeScale = Mathf.Lerp(currentTimeScale, 1f, progress);
                progress += Time.deltaTime * increaseSpeed;
                yield return null;
            }

            _playerInput.GetTargetDirection();
            Time.timeScale = 1f;
            _isBulletTime = false;
        }
    }
}
