using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Weapon
    {
        public Transform FirePoint { get => _firePoint; }
        public float BulletSpeed { get => _bulletSpeed; }
        public int BulletDamage { get => _bulletDamage; }

        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private float _bulletSpeed;

        [SerializeField]
        private int _bulletDamage;

        private float _fireTime;

        public bool TryFire(float cooldown)
        {
            float time = Time.time;
            if (time - _fireTime < cooldown)
                return false;

            _fireTime = time;
            return true;
        }
    }
}
