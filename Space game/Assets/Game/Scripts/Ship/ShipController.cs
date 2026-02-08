using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    // +
    public abstract class ShipController : MonoBehaviour
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDamage;
        public event Action OnDead;
        public event Action<ShipController, Weapon> OnFire;
        public ShipControllerSO Config { get => _config; }

        public int CurrentHealth { get; protected set; }
        public Vector3 MoveDirection { get; protected set; }

        [SerializeField]
        private ShipControllerSO _config;

        [SerializeField]
        private Weapon _weapon;

        [SerializeField]
        protected Motor _motor;

        

        private void Awake()
        {
            ResetHealth();
            _motor.SetSpeed(Config.MoveSpeed);
        }

        protected virtual void FixedUpdate() => _motor.FixedUpdate();

        protected void Fire()
        {
            if (this.CurrentHealth > 0 && _weapon.TryFire(Config.FireCooldown))
            {
                this.OnFire?.Invoke(this, _weapon);
            }
        }

        public void Hit(int damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, Config.Health);

            this.OnHealthChanged?.Invoke(CurrentHealth);
            if (CurrentHealth > 0)
            {
                this.OnDamage?.Invoke();
            }
            else
            {
                this.Dead();
            }
        }

        public void Dead()
        {
            this.OnDead?.Invoke();
            gameObject.SetActive(false);
        }

        protected void ResetHealth()
        {
            this.CurrentHealth = Config.Health;
        }
    }
}