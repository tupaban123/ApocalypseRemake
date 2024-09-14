using Apocalypse.Configs.Player;
using System;
using UnityEngine;

namespace Apocalypse.Player
{
    public class PlayerHealth
    {
        private PlayerHealthConfig _healthConfig;

        private float _currentHealth;
        public float CurrentHealth => _currentHealth;

        public Action Death;

        public PlayerHealth(PlayerHealthConfig cfg)
        {
            _healthConfig = cfg;

            _currentHealth = _healthConfig.MaxHP;
        }

        public void Damage(float damage)
        {
            _currentHealth -= damage;

            Debug.Log($"Damaged. Current health: {_currentHealth}");

            if (_currentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _currentHealth = 0;

            Death?.Invoke();
        }
    }
}