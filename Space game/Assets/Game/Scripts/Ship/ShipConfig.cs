using UnityEngine;

namespace Game
{
    // +
    [CreateAssetMenu(menuName = "Game/ShipControllerInfo", order = 0)]
    public sealed class ShipConfig : ScriptableObject
    {
        [Header("Core")]
        [field: SerializeField]
        public int Health { get; private set; } = 5;

        [field: SerializeField]
        public float MoveSpeed { get; private set; } = 5;

        [field: SerializeField]
        public TeamType Team { get; private set; }

        [Header("Weapon")]
        [field: SerializeField]
        public float FireCooldown { get; private set; } = 0.25f;

        [field: SerializeField]
        public float BulletSpeed { get; private set; } = 3.0f;

        [field: SerializeField]
        public int BulletDamage { get; private set; } = 1;
    }
}