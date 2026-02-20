using UnityEngine;

namespace Game
{
    public class PlayerWeapon : ShipWeapon
    {
        public override void Fire(ShipController shipController)
        {
            ShipControllerSO config = shipController.Config;

            _bulletManager.Spawn(
                new BulletController.RespawnArgs
                {
                    Position = _firePoint.position,
                    Direction = _firePoint.up,
                    Damage = config.BulletDamage,
                    Speed = config.BulletSpeed,
                    Team = TeamType.Player,
                }
            );
        }
    }
}