using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    // +
    public sealed class EnemyOrchestrator : MonoBehaviour, IEnemyDespawner
    {
        public Action<EnemyShip, int> OnEnemyDestroyed;
        public Action<EnemyShip> OnEnemySpawned;

        [Header("Spawn")]
        [SerializeField]
        private float _minSpawnCooldown = 2;

        [SerializeField]
        private float _maxSpawnCooldown = 3;

        [SerializeField]
        private EnemyPool _enemyPool;

        [SerializeField]
        private BulletManager _bulletManager;
        
        private float _spawnCooldown;
        private float _spawnTime;

        [Header("Target")]
        [SerializeField]
        private ShipController _player;

        [Header("Points")]
        [SerializeField]
        private PositionIterator _spawnPositions;

        [SerializeField]
        private PositionIterator _attackPositions;
        
        private int _destroyedEnemies;
        
        private void Start()
        {
            _spawnPositions.Shuffle();
            _attackPositions.Shuffle();

            ResetSpawnCooldown();
        }

        private void FixedUpdate()
        {
            TrySpawn();
        }

        private void ResetSpawnCooldown()
        {
            _spawnCooldown = UnityEngine.Random.Range(_minSpawnCooldown, _maxSpawnCooldown);
            _spawnTime = Time.fixedTime;
        }

        private bool TrySpawn()
        {
            float time = Time.fixedTime;
            if (time - _spawnTime < _spawnCooldown || ! _player.Health.IsAlive())
                return false;

            EnemyShip enemy = _enemyPool.Spawn();
            enemy.Respawn( new EnemyShip.RespawnArgs {
                target = _player,
                spawnPosition = _spawnPositions.Next(),
                destination = _attackPositions.Next() 
            });
            enemy.Weapon.SetBulletManager(_bulletManager);

            enemy.OnDead += Despawn;
            OnEnemySpawned?.Invoke(enemy);

            ResetSpawnCooldown();
            return true;
        }

        public void Despawn(EnemyShip enemy)
        {
            _destroyedEnemies++;
            enemy.OnDead -= Despawn;
            OnEnemyDestroyed?.Invoke(enemy, _destroyedEnemies);
            StartCoroutine(DespawnInNextFrame(enemy));
        }

        private IEnumerator DespawnInNextFrame(EnemyShip enemy)
        {
            yield return null;
            _enemyPool.Despawn(enemy);
        }
    }
}