using System;
using UnityEngine;

namespace Apocalypse.InputSystem
{
    public interface IInputSystem
    {
        Vector2 MoveAxis { get; }
        Vector2 AttackAxis { get; }

        Action DoubleClick { get; set; }
    }
}