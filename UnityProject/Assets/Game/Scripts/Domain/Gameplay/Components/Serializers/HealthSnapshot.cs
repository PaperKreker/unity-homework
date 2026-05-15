using SampleGame.SaveSystem;

namespace SampleGame.Gameplay.Serializers
{
    [System.Serializable]
    public class HealthSnapshot : ISnapshot<Health>
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