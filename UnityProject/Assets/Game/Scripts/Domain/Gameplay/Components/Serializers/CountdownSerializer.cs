using SampleGame.SaveSystem;

namespace SampleGame.Gameplay.Serializers
{
    public readonly struct CountdownSerializer : ISaveSerializer<CountdownSerializer.Snapshot>
    {
        public string Key => "Countdown";
        private readonly Countdown _countdown;

        public CountdownSerializer(Countdown countdown)
        {
            _countdown = countdown;
        }
        
        public Snapshot Serialize()
        {
            return new Snapshot(_countdown);
        }

        public void Deserialize(Snapshot snapshot)
        {
            snapshot.Restore(_countdown);
        }
        
        public struct Snapshot
        {
            public float Current;

            public Snapshot(Countdown countdown)
            {
                Current = countdown.Current;
            }

            public void Restore(Countdown countdown)
            {
                countdown.Current = Current;
            }
        }
    }
}