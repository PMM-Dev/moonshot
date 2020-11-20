using UnityEngine;

namespace Player
{
    public class PlayerSimulation
    {
        public float MovePosition(LookDirection lookDirection, float currentSpeed)
        {
            return (int)lookDirection * currentSpeed;
        }

        public float GetCurrentSpeed(bool isAccel, LookDirection lookDirection, float currentSpeed, float speed, float acceleration, float deceleration, bool isGround)
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

        public LookDirection GetLookDirection(LookDirection lookDirection, MoveDirection moveDirection, float currentSpeed, StickDirection stickDirection)
        {
            if (moveDirection == MoveDirection.Idle || stickDirection != StickDirection.Idle)
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

        public Vector2 Jump(Vector2 direction, float jumpPower)
        {
            return direction * jumpPower;
            // return direction * jumpPower * Mathf.Cos(Mathf.Atan2(direction.x, direction.y)) * 1000f * Time.fixedDeltaTime;s
        }
    }
}
