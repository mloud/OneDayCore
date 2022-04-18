using System;
using UnityEngine;

namespace OneDay.Games.Jumper
{
    public class PlayerInput : MonoBehaviour
    {
        public enum Type
        {
            AlwaysRun,
            Keyboard,
            Manual
        }

        public Type InputType;
        public float MoveValue;

        public float GetMoveValue()
        {
            switch (InputType)
            {
                case Type.Keyboard:
                    return Input.GetAxis("Horizontal");
                case Type.AlwaysRun:
                    return 1;
                case Type.Manual:
                    return MoveValue;
                default:
                    throw new NotSupportedException($"Move type {InputType} is not supported");
            }
        }
    }
}