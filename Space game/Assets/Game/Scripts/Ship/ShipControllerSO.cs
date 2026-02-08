using UnityEngine;

namespace Game
{
    // +
    [CreateAssetMenu(menuName = "Game/ShipControllerInfo", order = 0)]
    public sealed class ShipControllerSO : ScriptableObject
    {
        [Header("Core")]
        public int Health { get => _health; }
        public float MoveSpeed { get => _moveSpeed; }
        public float FireCooldown { get => _fireCooldown; }

        [SerializeField]
        private int _health = 5;

        [SerializeField]
        private float _moveSpeed = 5;

        [SerializeField]
        private float _fireCooldown = 0.25f;
    }
}