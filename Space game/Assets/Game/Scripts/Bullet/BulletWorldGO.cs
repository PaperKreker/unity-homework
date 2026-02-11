using System;
using System.Collections.Generic;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    // +
    public sealed class BulletWorldGO : MonoBehaviour
    {
        [SerializeField]
        private TransformBounds _levelBounds;

        [SerializeField]
        private ObjectPool _bulletPool;

        public void Spawn(Vector2 position, Vector2 direction, float speed, int damage, TeamType team)
        {
            BulletController bullet = _bulletPool.Spawn() as BulletController;
            bullet.OnDestroy += Despawn;
            bullet.Respawn(_levelBounds, position, team, direction, speed, damage);
        }

        private void Despawn(BulletController bullet)
        {
            _bulletPool.Despawn(bullet);
            bullet.OnDestroy -= Despawn;
        }
    }
}