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
        private MoveDirection _moveDirection;

        private PlayerLogic _playerLogic;
        private PlayerSimulation _playerSimulation;
        private PlayerInput _playerInput;

        private Rigidbody2D _rigidbody2D;

        private bool _isMove;

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
            if ((_moveDirection = _playerLogic.GetMoveDirection()) != MoveDirection.Idle)
            {
                _isMove = true;
            }
            else
            {
                _isMove = false;
            }
        }
        private void FixedUpdate()
        {
            if (_isMove)
            {
                Move();
            }
        }

        private void Move()
        {
            _rigidbody2D.MovePosition(_playerSimulation.MovePosition(_rigidbody2D, _moveDirection, _speed, _acceleration));
        }
    }
}
