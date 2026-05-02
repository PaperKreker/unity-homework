namespace SampleGame.SaveSystem
{
    public interface ISaveable
    {
        public ISaveSerializer Serializer { get; }
    }
}