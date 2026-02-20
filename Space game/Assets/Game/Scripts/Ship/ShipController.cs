using System;
using UnityEngine;

namespace Game
{
    // +
    public abstract class ShipController : MonoBehaviour
    {
        public event Action OnMove;
        
        public ShipControllerSO Config { get => _config; }

        public abstract TeamType Team { get; }

        [field: SerializeField]
        public ShipWeapon Weapon { get; private set; }

        [field: SerializeField]
        public ShipHealth Health { get; private set; }

        [SerializeField]
        private ShipControllerSO _config;


        protected void TryFire()
        {
            if (Health.IsAlive())
            {
                Weapon.TryFire(this);
            }
        }

        public void Hit(int damage)
        {
            Health.Hit(damage);
        }

        protected void Move()
        {
            OnMove?.Invoke();
        }
    }
}