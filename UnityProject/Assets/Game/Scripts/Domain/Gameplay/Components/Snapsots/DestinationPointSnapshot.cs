using SampleGame.Common;

namespace SampleGame.Gameplay.Snapshots
{
    [System.Serializable]
    public class DestinationPointSnapshot
    {
        public SerializedVector3 Value;

        public void Save(DestinationPoint destinationPoint)
        {
            Value = destinationPoint.Value;
        }

        public void Restore(DestinationPoint destinationPoint)
        {
            destinationPoint.Value = Value;
        }
    }
}