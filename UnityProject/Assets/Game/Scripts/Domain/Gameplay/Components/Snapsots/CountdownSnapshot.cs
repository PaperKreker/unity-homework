namespace SampleGame.Gameplay.Snapshots
{
    [System.Serializable]
    public class CountdownSnapshot
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