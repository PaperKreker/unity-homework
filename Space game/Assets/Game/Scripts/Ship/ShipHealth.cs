using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ShipHealth
    {
        public event Action<int, int> OnHealthChanged;
        public event Action OnDamage;
        public event Action OnDead;

        private ShipConfig _config;
        private GameObject _gameObject;
        private int _currentHealth;

        public void Initialize(ShipConfig config, GameObject gameObject)
        {
            _gameObject = gameObject;
            _config = config;
            ResetHealth();
        }

        public void Hit(int damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _config.Health);

            OnHealthChanged?.Invoke(_currentHealth, _config.Health);
            if (_currentHealth > 0)
            {
                OnDamage?.Invoke();
            }
            else
            {
                Dead();
            }
        }
        public void ResetHealth()
        {
            _currentHealth = _config.Health;
        }

        private void Dead()
        {
            OnDead?.Invoke();
            _gameObject.SetActive(false);
        }

        public bool IsAlive()
        {
            return _currentHealth > 0;
        }
    }
}
