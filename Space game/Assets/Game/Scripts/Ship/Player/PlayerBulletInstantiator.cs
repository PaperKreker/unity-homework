using UnityEngine;

namespace Game
{
    // +
    public sealed class PlayerBulletInstantiator : MonoBehaviour
    {
        [SerializeField]
        private BulletWorldGO _bulletWorld;

        [SerializeField]
        private PlayerShip _player;

        private void OnEnable()
        {
            _player.OnFire += this.OnFire;
        }

        private void OnDisable()
        {
            _player.OnFire -= this.OnFire;
        }

        private void OnFire(ShipController _, Weapon weapon)
        {
            _bulletWorld.Spawn(
                weapon.FirePoint.position,
                weapon.FirePoint.up,
                weapon.BulletSpeed,
                weapon.BulletDamage,
                TeamType.Player
            );
        }
    }
}