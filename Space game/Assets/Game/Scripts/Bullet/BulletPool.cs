using UnityEngine;

namespace Game
{
    public class BulletPool : ObjectPool<Bullet>
    {
        protected override void EnableObject(Bullet objectToEnable)
        {
            objectToEnable.gameObject.SetActive(true);
        }

        protected override void DisableObject(Bullet objectToDisable)
        {
            objectToDisable.gameObject.SetActive(false);
        }
    }
}
