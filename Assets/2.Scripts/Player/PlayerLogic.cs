using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLogic
    {
        private PlayerSimulation _playerSimulation;
        private PlayerInput _playerInput;

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

        public MoveDirection GetMoveInput()
        {
            if (_playerInput.IsInput(PressType.Stay, InputType.LeftMove))
            {
                return MoveDirection.Left;
            }
            else if (_playerInput.IsInput(PressType.Stay, InputType.RightMove))
            {
                return MoveDirection.Right;
            }
            else
            {
                return MoveDirection.Idle;
            }
        }

        public bool IsInput(PressType pressType, InputType inputType)
        {
            return _playerInput.IsInput(pressType, inputType);
        }

        public bool IsLookSameAsMove(LookDirection lookDirection, MoveDirection moveDirection)
        {
            return (int)moveDirection == (int)lookDirection;
        }

        public bool IsGround(CollisionType collisionType, Collider2D collider2D)
        {
            if (collider2D.tag.Equals("Ground"))
            {
                return collisionType == CollisionType.Exit ? false : true;
            }
            return false;
        }

        public StickDirection GetStickDirection(CollisionType collisionType, Collider2D collider2D, ColliderType colliderType, bool isGround)
        {
            if (collisionType != CollisionType.Exit)
            {
                if (collider2D.tag.Equals("Ground"))
                {
                    return (StickDirection)((int)colliderType);
                }
            }
            return StickDirection.Idle;
        }

        public MoveDirection GetMoveDirection(MoveDirection inputMoveDirection, StickDirection stickDiretion)
        {
            return IsMoveAvailable(inputMoveDirection, stickDiretion) ? inputMoveDirection : MoveDirection.Idle;
        }

        private bool IsMoveAvailable(MoveDirection moveDirection, StickDirection stickDirection)
        {
            return ((int)moveDirection != (int)stickDirection);
        }

        public bool IsJumpAvailable(bool isGround)
        {
            return isGround && IsInput(PressType.Stay, InputType.Jump);
        }
    }
}

