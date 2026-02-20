using UnityEngine;

namespace Game
{
    public class BulletPool : ObjectPool<BulletController>
    {
        protected override void EnableObject(BulletController objectToEnable)
        {
            objectToEnable.gameObject.SetActive(true);
        }

        protected override void DisableObject(BulletController objectToDisable)
        {
            objectToDisable.gameObject.SetActive(false);
        }
    }
}
