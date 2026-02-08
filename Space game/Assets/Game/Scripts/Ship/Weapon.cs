using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Weapon
    {
        public Transform FirePoint { get => firePoint; }
        public float BulletSpeed { get => bulletSpeed; }
        public int BulletDamage { get => bulletDamage; }

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private float bulletSpeed;

        [SerializeField]
        private int bulletDamage;

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
