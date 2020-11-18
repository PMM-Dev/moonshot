using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public enum InputType
    {
        None,
        LeftMove = -1,
        RightMove = 1,
        Jump,
        Attack,
        Climb
    }

    public enum ActionType
    {
        Move = 0,
        Jump,
        Attack,
        Climb,
        Count
    }

    public enum ColliderType
    {
        None,
        Left = -1,
        Right = 1,
        Bottom,
    }

    public enum CollisionType
    {
        Enter,
        Stay,
        Exit
    }

    public enum PressKeyType
    {
        Down,
        Stay,
        Up
    }

    public enum MoveDirection
    {
        Left = -1,
        Idle = 0,
        Right = 1
    }

    public enum LookDirection
    {
        Left = -1,
        Right = 1
    }

    public enum StickDirection
    {
        Left = -1,
        Idle = 0,
        Right = 1
    }

    public enum JumpState
    {
        None,
        Normal,
        Wall,
        Escape
    }

    public class Define
    {

    }
}
