using SampleGame.Common;
using SampleGame.SaveSystem;

namespace SampleGame.Gameplay.Serializers
{
    [System.Serializable]
    public class DestinationPointSnapshot : ISnapshot<DestinationPoint>
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