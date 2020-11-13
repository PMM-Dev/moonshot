﻿using UnityEngine;

namespace Player
{
    public class PlayerSimulation
    {
        public Vector2 MovePosition(Vector2 position, LookDirection lookDirection, float currentSpeed)
        {
            return position + Vector2.right * (int)lookDirection * currentSpeed * Time.fixedDeltaTime;
        }

        public float GetCurrentSpeed(bool isAccel, LookDirection lookDirection, float currentSpeed, float speed, float acceleration, float deceleration)
        {
            if (isAccel)
            {
                currentSpeed += acceleration;
                return Mathf.Clamp(currentSpeed, 0, speed);
            }
            else
            {
                currentSpeed -= deceleration;
                return Mathf.Clamp(currentSpeed, 0, speed);
            }
        }

        public LookDirection GetLookDirection(LookDirection lookDirection, MoveDirection moveDirection, float currentSpeed)
        {
            if (moveDirection == MoveDirection.Idle)
                return lookDirection;

            if (currentSpeed == 0)
            {
                return (LookDirection)((int)lookDirection * -1);
            }
            else
            {
                return lookDirection;
            }
        }

        public Vector2 Jump(Rigidbody2D rigidbody2D, Vector2 direction, float jumpPower)
        {
            return new Vector2(rigidbody2D.position.x,  rigidbody2D.position.y + jumpPower) * Time.fixedDeltaTime;
        }
    }
}

