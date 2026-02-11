using System;
using System.Collections;
using System.Collections.Generic;
using Modules.UI;
using Modules.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

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
        private ObjectPool _enemyPool;
        
        private float _spawnCooldown;
        private float _spawnTime;

        [Header("Target")]
        [SerializeField]
        private ShipController _player;

        [Header("Points")]
        [SerializeField]
        private PositionRandomizer _spawnPositions;

        [SerializeField]
        private PositionRandomizer _attackPositions;
        
        private int _destroyedEnemies;
        
        private void Start()
        {
            _spawnPositions.Shaffle();
            _attackPositions.Shaffle();

            this.ResetSpawnCooldown();
        }

        private void FixedUpdate()
        {
            TrySpawn();
        }

        private void ResetSpawnCooldown()
        {
            _spawnCooldown = Random.Range(_minSpawnCooldown, _maxSpawnCooldown);
            _spawnTime = Time.fixedTime;
        }

        private bool TrySpawn()
        {
            float time = Time.fixedTime;
            if (time - _spawnTime < _spawnCooldown || _player.CurrentHealth <= 0)
                return false;

            EnemyShip enemy = _enemyPool.Spawn() as EnemyShip;
            enemy.Respawn(
                _player,
                _spawnPositions.Next(),
                _attackPositions.Next());

            enemy.SetDespawner(this);
            OnEnemySpawned?.Invoke(enemy);

            this.ResetSpawnCooldown();
            return true;
        }

        public void Despawn(EnemyShip enemy)
        {
            _destroyedEnemies++;
            OnEnemyDestroyed?.Invoke(enemy, _destroyedEnemies);
            this.StartCoroutine(DespawnInNextFrame(enemy));
        }

        private IEnumerator DespawnInNextFrame(EnemyShip enemy)
        {
            yield return null;
            _enemyPool.Despawn(enemy);
        }
    }
}