using Modules.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    // +
    public sealed class BulletManager : MonoBehaviour
    {
        public Action<Bullet> OnSpawn;
        public Action<Bullet> OnDespawn;

        [SerializeField]
        private BulletPool _bulletPool;

        [SerializeField]
        private TransformBounds _levelBounds;

        private List<Bullet> _activeBullets = new List<Bullet>();

        private void FixedUpdate()
        {
            MoveBullets();
            CheckBulletsBounds();
        }

        private void MoveBullets()
        {
            for (int i = 0; i < _activeBullets.Count; ++i)
            {
                _activeBullets[i].Move();
            }
        }

        private void CheckBulletsBounds()
        {
            for (int i = 0; i < _activeBullets.Count; ++i)
            {
                if (!_levelBounds.InBounds(_activeBullets[i].transform.position))
                {
                    Despawn(_activeBullets[i]);
                    --i;
                }
            }
        }

        public void Spawn(Vector2 direction, Vector2 position, ShipConfig config)
        {
            Bullet bullet = _bulletPool.Spawn();
            bullet.OnDestroy += Despawn;
            bullet.Respawn(direction, position, config);

            _activeBullets.Add(bullet);

            OnSpawn?.Invoke(bullet);
        }

        public void Despawn(Bullet bullet)
        {
            _bulletPool.Despawn(bullet);
            bullet.OnDestroy -= Despawn;

            _activeBullets.Remove(bullet);

            OnDespawn?.Invoke(bullet);
        }
    }
}