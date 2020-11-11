using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLogic
    {
        private PlayerSimulation _playerSimulation;
        private PlayerInput _playerInput;

        private PlayerController _playerController;

        public PlayerLogic(PlayerSimulation playerSimulation, PlayerInput playerInput)
        {
            _playerSimulation = playerSimulation;
            _playerInput = playerInput;
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public MoveDirection GetMoveDirection()
        {
            if (_playerInput.GetKey(InputType.LeftMove))
                return MoveDirection.Left;
            else if(_playerInput.GetKey(InputType.RightMove))
                return MoveDirection.Right;
            else
                return MoveDirection.Idle;
        }

        
    }
}

