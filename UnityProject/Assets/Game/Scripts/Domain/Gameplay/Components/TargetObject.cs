using Modules.Entities;
using SampleGame.Gameplay.Serializers;
using SampleGame.SaveSystem;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class TargetObject : MonoBehaviour, ISaveable
    {
        ///Variable
        [field: SerializeField]
        public Entity Value { get; set; }

        public ISaveSerializer Serializer => new TargetObjectSerializer(this);
    }
}