using System;
using UnityEngine;

namespace Game
{
    public abstract class ShipWeapon : MonoBehaviour
    {
        public event Action<ShipController, Transform> OnFire;

        [SerializeField]
        protected Transform _firePoint;

        [SerializeField]
        protected BulletManager _bulletManager;

        private float _fireTime;

        public void SetBulletManager(BulletManager bulletManager)
        {
            _bulletManager = bulletManager;
        }

        public bool TryFire(ShipController shipController)
        {
            float time = Time.time;
            if (time - _fireTime < shipController.Config.FireCooldown)
                return false;

            _fireTime = time;

            Fire(shipController);
            OnFire?.Invoke(shipController, _firePoint);
            return true;
        }

        public abstract void Fire(ShipController shipController);
    }
}
