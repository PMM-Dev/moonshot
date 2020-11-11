using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerSimulation
    {
        private float _currentSpeed;

        public PlayerSimulation()
        {

        }

        public void FixedUpdate()
        {

        }

        public Vector2 MovePosition(Rigidbody2D rigidbody2D, MoveDirection moveDirection, float speed, float acceleration)
        {
            _currentSpeed += acceleration;

            return rigidbody2D.position + Vector2.right * (int)moveDirection * _currentSpeed * Time.fixedDeltaTime;
        }
    }
}

