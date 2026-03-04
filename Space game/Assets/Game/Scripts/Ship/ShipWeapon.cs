using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ShipWeapon
    {
        public event Action<Transform> OnFire;

        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private BulletManager _bulletManager;

        private ShipConfig _config;
        private float _fireTime;

        public void Initialize(ShipConfig config)
        {
            _config = config;
        }

        public void SetBulletManager(BulletManager bulletManager)
        {
            _bulletManager = bulletManager;
        }

        private bool TryFire()
        {
            float time = Time.time;
            if (time - _fireTime < _config.FireCooldown)
                return false;

            _fireTime = time;
            
            return true;
        }

        public void FireForward()
        {
            if (TryFire())
            {
                _bulletManager.Spawn(_firePoint.up, _firePoint.position, _config);

                OnFire?.Invoke(_firePoint);
            }
        }

        public void FireTarget(Transform target)
        {
            if (TryFire())
            {
                Vector2 firePosition = _firePoint.position;
                Vector2 targetPosition = target.position;
                Vector2 direction = (targetPosition - firePosition).normalized;

                _bulletManager.Spawn(direction, firePosition, _config);

                OnFire?.Invoke(_firePoint);
            }
        }
    }
}
