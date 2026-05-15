namespace SampleGame.Gameplay.Snapshots
{
    [System.Serializable]
    public class HealthSnapshot
    {
        public int Current;

        public void Save(Health health)
        {
            Current = health.Current;
        }

        public void Restore(Health health)
        {
            health.Current = Current;
        }
    }
}