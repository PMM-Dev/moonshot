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

        public MoveDirection GetMoveInput()
        {
            if (_playerInput.IsInput(PressKeyType.Stay, InputType.LeftMove))
            {
                return MoveDirection.Left;
            }
            else if (_playerInput.IsInput(PressKeyType.Stay, InputType.RightMove))
            {
                return MoveDirection.Right;
            }
            else
            {
                return MoveDirection.Idle;
            }
        }

        public bool IsInput(PressKeyType pressType, InputType inputType)
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

        public StickDirection GetStickDirection(CollisionType collisionType, Collider2D collider2D, ColliderType colliderType, bool isGround, MoveDirection moveDirection)
        {
            if (collisionType != CollisionType.Exit)
            {
                if (collider2D.tag.Equals("Ground") && (int)colliderType == (int)moveDirection)
                {
                    return (StickDirection)((int)colliderType);
                }
            }
            return StickDirection.Idle;
        }

        public MoveDirection GetMoveDirection(MoveDirection currentMoveDirection, MoveDirection inputMoveDirection, StickDirection stickDiretion, bool isGround, bool isMoveInputLocked)
        {
            return IsMoveAvailable(inputMoveDirection, stickDiretion, isMoveInputLocked) ? inputMoveDirection : MoveDirection.Idle;
        }

        private bool IsMoveAvailable(MoveDirection moveDirection, StickDirection stickDirection, bool isMoveInputLocked)
        {
            return !isMoveInputLocked;
        }

        public JumpState GetJumpState(bool isJumpInputLocked, bool isGround, MoveDirection _moveDirection, StickDirection stickDirection)
        {
            if (IsInput(PressKeyType.Down, InputType.Jump))
            {
                if (!isGround && stickDirection != StickDirection.Idle)
                {
                    if ((int)GetMoveInput() == (int)stickDirection)
                    {
                        return JumpState.Wall;
                    }
                }
                else if (isGround)
                    return JumpState.Normal;
            }
            return JumpState.None;
        }

        public Vector2 GetJumpDiretion(JumpState jumpState, StickDirection stickDirection)
        {
            if (jumpState == JumpState.Normal)
                return Vector2.up;
            else
            {
                float angle = 60f;
                angle = stickDirection == StickDirection.Left ? angle : -angle;
                Vector2 lDirection = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));
                return lDirection.normalized;
            }
        }
    }
}

