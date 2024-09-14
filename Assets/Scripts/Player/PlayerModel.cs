using Apocalypse.Configs.Player;
using Apocalypse.Player.Movement;
using System;
using UnityEngine;
using Zenject;

namespace Apocalypse.Player
{
    public class PlayerModel
    {
        private PlayerMovement _playerMovement;
        private PlayerHealth _playerHealth;
        private PlayerView _playerView;
        private PlayerConfig _playerConfig;
        
        private Rigidbody _playerRb;

        public Action DoubleClickCallback => _playerMovement.StartDash;

        public PlayerModel(PlayerView playerView, PlayerConfig playerConfig, Rigidbody playerRb)
        {
            _playerView = playerView;
            _playerConfig = playerConfig;
            _playerRb = playerRb;

            _playerMovement = new PlayerMovement(playerView, playerConfig.MovementConfig, playerRb);
            _playerHealth = new PlayerHealth(_playerConfig.HealthConfig);
        }

        public void Dispose()
        {
            _playerMovement.Dispose();
        }

        public void Damage(float damage) => _playerHealth.Damage(damage);
    }
}