using System;
using UnityEngine;

namespace Game
{
    // +
    public class Ship : MonoBehaviour
    {
        public TeamType Team { get => _config.Team; }

        [SerializeField]
        private ShipConfig _config;

        [SerializeField]
        private ShipWeapon _weapon;
        public event Action<Transform> OnFire
        {
            add => _weapon.OnFire += value;
            remove => _weapon.OnFire -= value;
        }

        [SerializeField]
        private ShipHealth _health;
        public event Action<int, int> OnHealthChanged
        {
            add => _health.OnHealthChanged += value;
            remove => _health.OnHealthChanged -= value;
        }
        public event Action OnDamage
        {
            add => _health.OnDamage += value;
            remove => _health.OnDamage -= value;
        }
        public event Action OnDead
        {
            add => _health.OnDead += value;
            remove => _health.OnDead -= value;
        }

        [SerializeField]
        private ShipMovement _movement;
        public Vector2 Direction { get { return _movement.Direction; }}


        private void Awake()
        {
            _movement.Initialize(_config);
            _health.Initialize(_config, gameObject);
            _weapon.Initialize(_config);
        }

        // Battle
        public void FireForward()
        {
            if (_health.IsAlive())
            {
                _weapon.FireForward();
            }
        }
        
        public void SetBulletManager(BulletManager bulletManager)
        {
            _weapon.SetBulletManager(bulletManager);
        }

        public void FireTarget(Transform target)
        {
            if (_health.IsAlive())
            {
                _weapon.FireTarget(target);
            }
        }

        // Move
        public void Move(Vector2 direction)
        {
            if (_health.IsAlive())
            {
                _movement.MoveStep(direction);
            }
        }

        // Health
        public void ResetHealth()
        {
            _health.ResetHealth();
        }

        public void Hit(int damage)
        {
            _health.Hit(damage);
        }

        public bool IsAlive()
        {
            return _health.IsAlive();
        }
    }
}