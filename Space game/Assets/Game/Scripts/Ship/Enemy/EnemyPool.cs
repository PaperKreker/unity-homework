
using UnityEngine;

namespace Game
{
    public class EnemyPool : ObjectPool<EnemyBehaviour>
    {
        [SerializeField]
        private BulletManager _bulletManager;

        protected override EnemyBehaviour CreateObject()
        {
            EnemyBehaviour enemyShip = base.CreateObject();
            enemyShip.GetComponent<Ship>().SetBulletManager(_bulletManager);
            return enemyShip;
        }

        protected override void EnableObject(EnemyBehaviour objectToEnable)
        {
            objectToEnable.gameObject.SetActive(true);
        }

        protected override void DisableObject(EnemyBehaviour objectToDisable)
        {
            objectToDisable.gameObject.SetActive(false);
        }
    }
}
