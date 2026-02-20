namespace Game
{
    public class EnemyPool : ObjectPool<EnemyShip>
    {
        protected override void EnableObject(EnemyShip objectToEnable)
        {
            objectToEnable.gameObject.SetActive(true);
        }

        protected override void DisableObject(EnemyShip objectToDisable)
        {
            objectToDisable.gameObject.SetActive(false);
        }
    }
}
