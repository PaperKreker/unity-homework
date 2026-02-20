using UnityEngine;

namespace Game
{
    // +
    [CreateAssetMenu(menuName = "Game/ShipControllerInfo", order = 0)]
    public sealed class ShipControllerSO : ScriptableObject
    {
        public int Health { get => _health; }
        public float MoveSpeed { get => _moveSpeed; }
        public float FireCooldown { get => _fireCooldown; }
        public float BulletSpeed { get => _bulletSpeed; }
        public int BulletDamage { get => _bulletDamage; }

        [Header("Core")]
        [SerializeField]
        private int _health = 5;

        [SerializeField]
        private float _moveSpeed = 5;

        [Header("Weapon")]
        [SerializeField]
        private float _fireCooldown = 0.25f;

        [SerializeField]
        private float _bulletSpeed = 3.0f;

        [SerializeField]
        private int _bulletDamage = 1;
    }
}