using SampleGame.SaveSystem;

namespace SampleGame.Gameplay.Serializers
{
    public readonly struct HealthSerializer : ISaveSerializer<HealthSerializer.Snapshot>
    {
        public string Key => "Health";
        private readonly Health _health;

        public HealthSerializer(Health health)
        {
            _health = health;
        }
        
        public Snapshot Serialize()
        {
            return new Snapshot(_health);
        }

        public void Deserialize(Snapshot snapshot)
        {
            snapshot.Restore(_health);
        }
        
        public struct Snapshot
        {
            public int Current;

            public Snapshot(Health health)
            {
                Current = health.Current;
            }

            public void Restore(Health health)
            {
                health.Current = Current;
            }
        }
    }
}