using SampleGame.Common;
using SampleGame.Gameplay.Serializers;
using SampleGame.SaveSystem;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class ResourceBag : MonoBehaviour, ISaveable
    {
        ///Variable
        [field: SerializeField]
        public ResourceType Type { get; set; }
        
        ///Variable
        [field: SerializeField]
        public int Current { get; set; }
        
        ///Const
        [field: SerializeField]
        public int Capacity { get; set; }
        
        public ISaveSerializer Serializer => new ResourceBagSerializer(this);
    }
}