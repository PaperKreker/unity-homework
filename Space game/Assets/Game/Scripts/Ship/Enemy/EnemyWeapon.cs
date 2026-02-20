using UnityEngine;

namespace Game
{
    public class EnemyWeapon : ShipWeapon
    {
        public override void Fire(ShipController shipController)
        {
            EnemyShip enemyShip = (EnemyShip)shipController;
            ShipControllerSO config = enemyShip.Config;

            Vector2 position = _firePoint.position;
            Vector2 target = enemyShip.Target.transform.position;
            Vector2 direction = (target - position).normalized;

            _bulletManager.Spawn(
                new BulletController.RespawnArgs
                {
                    Position = _firePoint.position,
                    Damage = config.BulletDamage,
                    Speed = config.BulletSpeed,
                    Direction = direction,
                    Team = TeamType.Enemy,
                }
            );
        }
    }
}