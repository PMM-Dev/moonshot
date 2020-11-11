using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInput
    {
        private Dictionary<InputType, KeyCode> _inputKeys;

        public PlayerInput()
        {
            _inputKeys = new Dictionary<InputType, KeyCode>()
            {
                { InputType.RightMove, KeyCode.RightArrow },
                { InputType.LeftMove, KeyCode.LeftArrow }
            };
        }

        public bool GetKeyUp(InputType inputType)
        {
            return Input.GetKeyUp(_inputKeys[inputType]);
        }
        public bool GetKeyDown(InputType inputType)
        {
            return Input.GetKeyDown(_inputKeys[inputType]);
        }
        public bool GetKey(InputType inputType)
        {
            return Input.GetKey(_inputKeys[inputType]);
        }
    }
}

