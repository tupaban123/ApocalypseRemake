using System;
using UnityEngine;

namespace Apocalypse.InputSystem
{
    public class TouchInputSystem : IInputSystem, IDisposable
    {
        private Joystick _moveJoystick;
        private Joystick _attackJoystick;

        private float _lastClickTime;

        public TouchInputSystem(IMoveJoystick moveJoystick, IAttackJoystick attackJoystick)
        {
            _moveJoystick = moveJoystick as Joystick;
            _attackJoystick = attackJoystick as Joystick;

            _moveJoystick.OnDown += OnMoveJoystickDown;
        }

        public void Dispose()
        {
            _moveJoystick.OnDown -= OnMoveJoystickDown;
        }

        public Vector2 MoveAxis => new Vector2(_moveJoystick.Horizontal, _moveJoystick.Vertical);

        public Vector2 AttackAxis => new Vector2(_attackJoystick.Horizontal, _attackJoystick.Vertical);

        public Action DoubleClick { get; set; }

        private void OnMoveJoystickDown()
        {
            if (Time.time - _lastClickTime < .2f)
            {
                DoubleClick?.Invoke();
            }

            _lastClickTime = Time.time;
        }
    }
}