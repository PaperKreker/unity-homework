using UnityEngine;

namespace Game
{
    // +
    public sealed class EnemyBulletInstantiator : MonoBehaviour
    {
        [SerializeField] 
        private EnemyOrchestrator _enemyOrchestrator;

        [SerializeField]
        private BulletWorldGO _bulletWorld;

        private void OnEnable()
        {
            _enemyOrchestrator.OnEnemySpawned += BindFire;
            _enemyOrchestrator.OnEnemyDestroyed += UnbindFire;
        }

        private void OnDisable()
        {
            _enemyOrchestrator.OnEnemySpawned -= BindFire;
            _enemyOrchestrator.OnEnemyDestroyed -= UnbindFire;
        }

        private void BindFire(EnemyShip enemyShip)
        {
            enemyShip.OnFire += Fire;
        }

        private void UnbindFire(EnemyShip enemyShip, int _)
        {
            enemyShip.OnFire -= Fire;
        }

        private void Fire(ShipController shipController, Weapon weapon)
        {
            EnemyShip enemyShip = (EnemyShip)shipController;

            Vector2 position = weapon.FirePoint.position;
            Vector2 target = enemyShip.Target.transform.position;
            Vector2 direction = (target - position).normalized;
            _bulletWorld.Spawn(
                weapon.FirePoint.position,
                direction,
                weapon.BulletSpeed,
                weapon.BulletDamage,
                TeamType.Enemy
            );
        }
    }
}