using SampleGame.SaveSystem;

namespace SampleGame.Gameplay.Serializers
{
    [System.Serializable]
    public class CountdownSnapshot : ISnapshot<Countdown>
    {
        public float Current;

        public void Save(Countdown countdown)
        {
            Current = countdown.Current;
        }

        public void Restore(Countdown countdown)
        {
            countdown.Current = Current;
        }
    }
}