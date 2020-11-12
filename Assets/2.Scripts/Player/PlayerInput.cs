using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInput
    {
        private Dictionary<InputType, KeyCode> _inputKeys;
        private Dictionary<PressType, Func<InputType, bool>> _getKeys;

        public PlayerInput()
        {
            _inputKeys = new Dictionary<InputType, KeyCode>()
            {
                { InputType.LeftMove, KeyCode.A },
                { InputType.RightMove, KeyCode.D },
                { InputType.Jump, KeyCode.Space }
            };

            _getKeys = new Dictionary<PressType, Func<InputType, bool>>()
            {
                { PressType.Down, (x) => GetKeyDown(x) },
                { PressType.Stay, (x) => GetKey(x) },
                { PressType.Up, (x) => GetKeyUp(x) }
            };
        }

        public bool IsInput(PressType pressType, InputType inputType)
        {
            return _getKeys[pressType](inputType);
        }

        private bool GetKeyUp(InputType inputType)
        {
            return Input.GetKeyUp(_inputKeys[inputType]);
        }
        private bool GetKeyDown(InputType inputType)
        {
            return Input.GetKeyDown(_inputKeys[inputType]);
        }
        private bool GetKey(InputType inputType)
        {
            return Input.GetKey(_inputKeys[inputType]);
        }
    }
}

