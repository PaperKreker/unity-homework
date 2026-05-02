using SampleGame.Common;
using SampleGame.SaveSystem;

namespace SampleGame.Gameplay.Serializers
{
    public readonly struct DestinationPointSerializer : ISaveSerializer<DestinationPointSerializer.Snapshot>
    {
        public string Key => "DestinationPoint";
        private readonly DestinationPoint _destinationPoint;

        public DestinationPointSerializer(DestinationPoint destinationPoint)
        {
            _destinationPoint = destinationPoint;
        }
        
        public Snapshot Serialize()
        {
            return new Snapshot(_destinationPoint);
        }

        public void Deserialize(Snapshot snapshot)
        {
            snapshot.Restore(_destinationPoint);
        }
        
        public struct Snapshot
        {
            public SerializableVector3 Value;

            public Snapshot(DestinationPoint destinationPoint)
            {
                Value = destinationPoint.Value;
            }

            public void Restore(DestinationPoint destinationPoint)
            {
                destinationPoint.Value = Value;
            }
        }
    }
}