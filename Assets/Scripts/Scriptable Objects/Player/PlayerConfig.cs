using System;
using UnityEngine;

namespace Apocalypse.Configs.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerHealthConfig HealthConfig;
        [field: SerializeField] public PlayerMovementConfig MovementConfig;
    }

    [Serializable]
    public class PlayerHealthConfig
    {
        [field: SerializeField] public float MaxHP { get; private set; }
    }

    [Serializable] 
    public class PlayerMovementConfig
    {
        [Header("Movement")]
        [field: SerializeField] public float Speed { get; private set; } = 5f;
        [field: SerializeField] public float RotateSpeed { get; private set; } = 50f;

        [Header("Dash")]
        [field: SerializeField] public float DashSpeedMultiplier { get; private set; } = 2f;
        [field: SerializeField] public float DashDuration { get; private set; } = 1.5f;
        [field: SerializeField] public float DashCooldown { get; private set; } = 2f;
    }
}