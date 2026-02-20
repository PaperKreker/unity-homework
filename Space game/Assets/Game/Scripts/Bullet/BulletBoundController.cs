using Modules.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BulletBoundController : MonoBehaviour
    {
        [SerializeField]
        private BulletManager _bulletManager;

        [SerializeField]
        private TransformBounds _levelBounds;

        private List<BulletController> _activeBullets = new List<BulletController>();

        private void OnEnable()
        {
            _bulletManager.OnSpawn += (BulletController bullet) => _activeBullets.Add(bullet);
            _bulletManager.OnDespawn += (BulletController bullet) => _activeBullets.Remove(bullet);
        }

        private void OnDisable()
        {
            _bulletManager.OnSpawn -= (BulletController bullet) => _activeBullets.Add(bullet);
            _bulletManager.OnDespawn -= (BulletController bullet) => _activeBullets.Remove(bullet);
        }

        private void FixedUpdate()
        {
            CheckBulletsBounds();
        }

        private void CheckBulletsBounds()
        {
            for (int i = 0; i < _activeBullets.Count; ++i)
            {
                if (!_levelBounds.InBounds(_activeBullets[i].transform.position))
                {
                    _bulletManager.Despawn(_activeBullets[i]);
                    --i;
                }
            }
        }
    }
}