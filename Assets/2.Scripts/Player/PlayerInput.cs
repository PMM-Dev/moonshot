using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Player
{
    public class PlayerInput
    {
        private Dictionary<InputType, KeyCode> _inputKeys;
        private Dictionary<PressKeyType, Func<InputType, bool>> _getKeys;

        private Vector2 _originMousePosition;
        private Vector2 _targetMousePosition;

        private bool _isPause;

        public void InitializeEvent()
        {
            if (MainEventManager.Instance != null)
            {
                MainEventManager.Instance.PauseGamePlayEvent += PauseGameEvent;
                MainEventManager.Instance.ResumeGamePlayEvent += ResumeGameEvent;
            }
        }

        public void PauseGameEvent()
        {
            _isPause = true;
        }

        public void ResumeGameEvent()
        {
            _isPause = false;
        }

        public PlayerInput()
        {
            _inputKeys = new Dictionary<InputType, KeyCode>()
            {
                { InputType.LeftMove, KeyCode.A },
                { InputType.RightMove, KeyCode.D },
                { InputType.Jump, KeyCode.Space },
            };

            _getKeys = new Dictionary<PressKeyType, Func<InputType, bool>>()
            {
                { PressKeyType.Down, (x) => GetKeyDown(x) },
                { PressKeyType.Stay, (x) => GetKey(x) },
                { PressKeyType.Up, (x) => GetKeyUp(x) }
            };
        }

        public bool IsInput(PressKeyType pressType, InputType inputType)
        {
            return _getKeys[pressType](inputType);
        }

        private bool GetKeyUp(InputType inputType)
        {
            if (_isPause) return false;
            return Input.GetKeyUp(_inputKeys[inputType]);
        }
        private bool GetKeyDown(InputType inputType)
        {
            if (_isPause) return false;
            return Input.GetKeyDown(_inputKeys[inputType]);
        }
        private bool GetKey(InputType inputType)
        {
            if (_isPause) return false;
            return Input.GetKey(_inputKeys[inputType]);
        }

        public void GetMouseDirection()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _originMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _targetMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        public bool GetMouseButtonUp()
        {
            if (_isPause) return false;
            return Input.GetMouseButtonUp(0);
        }

        public Vector2 GetSlashDirection()
        {
            return (_targetMousePosition - _originMousePosition).normalized;
        }

        public float GetSlashAngle()
        {
            return Mathf.Atan2(_targetMousePosition.x - _originMousePosition.x, _targetMousePosition.y - _originMousePosition.y) * Mathf.Rad2Deg;
        }

        public float GetMouseInputDistance()
        {
            return Vector2.Distance(_targetMousePosition, _originMousePosition);
        }
    }
}

