using SampleGame.Gameplay.Serializers;
using SampleGame.SaveSystem;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class DestinationPoint : MonoBehaviour, ISaveable
    {
        ///Variable
        [field: SerializeField]
        public Vector3 Value { get; set; }
        public ISaveSerializer Serializer => new DestinationPointSerializer(this);
    }
}