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
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _acceleration;
        [SerializeField]
        private float _deceleration;
        [SerializeField]
        private float _currentSpeed;

        [SerializeField]
        private MoveDirection _moveDirection;

        [SerializeField]
        private LookDirection _lookDirection = LookDirection.Right;

        private PlayerLogic _playerLogic;
        private PlayerSimulation _playerSimulation;
        private PlayerInput _playerInput;

        private Rigidbody2D _rigidbody2D;

        private bool _isAccel;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _playerSimulation = new PlayerSimulation();
            _playerInput = new PlayerInput();
            _playerLogic = new PlayerLogic(this._playerSimulation, this._playerInput);
        }

        private void Update()
        {
            _moveDirection = _playerLogic.GetMoveDirection();
            _isAccel = _playerLogic.IsLookSameAsMove(_lookDirection, _moveDirection);
        }
        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _currentSpeed = _playerSimulation.GetCurrentSpeed(_isAccel, _lookDirection, _currentSpeed, _speed, _acceleration, _deceleration);
            _lookDirection = _playerSimulation.GetLookDirection(_lookDirection, _moveDirection, _currentSpeed);
            transform.position = _playerSimulation.MovePosition(transform.position, _lookDirection, _currentSpeed);
        }
    }
}
