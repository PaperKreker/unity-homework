using System;
using UnityEngine;

namespace Game
{
    // +
    public sealed class BulletManager : MonoBehaviour
    {
        public Action<BulletController> OnSpawn;
        public Action<BulletController> OnDespawn;

        [SerializeField]
        private BulletPool _bulletPool;

        public void Spawn(BulletController.RespawnArgs respawnArgs)
        {
            BulletController bullet = _bulletPool.Spawn();
            bullet.OnDestroy += Despawn;
            bullet.Respawn(respawnArgs);

            OnSpawn?.Invoke(bullet);
        }

        public void Despawn(BulletController bullet)
        {
            _bulletPool.Despawn(bullet);
            bullet.OnDestroy -= Despawn;

            OnDespawn?.Invoke(bullet);
        }
    }
}