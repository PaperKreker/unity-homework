using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    // +
    public sealed class EnemySpawner : MonoBehaviour
    {
        public Action<EnemyBehaviour, int> OnEnemyDestroyed;

        [SerializeField]
        private EnemyPool _enemyPool;

        [SerializeField]
        private Timer _enemyTimer;

        [SerializeField]
        private EnemyPosition _enemyPosition;

        [Header("Target")]
        [SerializeField]
        private Ship _player;
        
        private int _destroyedEnemies;

        private void OnEnable()
        {
            _enemyTimer.OnAlert += Spawn;
        }

        private void OnDisable()
        {
            _enemyTimer.OnAlert -= Spawn;
        }

        private void Spawn()
        {
            if (_player.IsAlive())
            {
                _enemyTimer.ResetCooldown();
                EnemyBehaviour enemy = _enemyPool.Spawn();

                _enemyPosition.GetPositions(out Vector2 spawnPosition, out Vector2 destinationPosition);
                enemy.Respawn(_player, spawnPosition, destinationPosition);

                enemy.OnDead += Despawn;
            }
        }

        public void Despawn(EnemyBehaviour enemy)
        {
            _destroyedEnemies++;
            enemy.OnDead -= Despawn;
            OnEnemyDestroyed?.Invoke(enemy, _destroyedEnemies);
            StartCoroutine(DespawnInNextFrame(enemy));
        }

        private IEnumerator DespawnInNextFrame(EnemyBehaviour enemy)
        {
            yield return null;
            _enemyPool.Despawn(enemy);
        }
    }
}