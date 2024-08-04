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
        private PlayerView _playerView;
        private PlayerConfig _playerConfig;
        
        private Rigidbody _playerRb;

        public Action DoubleClickCallback => _playerMovement.DoubleClickCallback;

        public PlayerModel(PlayerView playerView, PlayerConfig playerConfig, Rigidbody playerRb)
        {
            _playerView = playerView;
            _playerConfig = playerConfig;
            _playerRb = playerRb;

            _playerMovement = new PlayerMovement(playerView, playerConfig, playerRb);
        }

        public void Dispose()
        {
            _playerMovement.Dispose();
        }
    }
}