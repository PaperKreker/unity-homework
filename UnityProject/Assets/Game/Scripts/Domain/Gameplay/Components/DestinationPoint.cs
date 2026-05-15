using Newtonsoft.Json.Linq;
using SampleGame.SaveSystem;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class DestinationPoint : MonoBehaviour, ISerializableEntity
    {
        ///Variable
        [field: SerializeField]
        public Vector3 Value { get; set; }
        
        public string Key => "DestinationPoint";
        public JToken Serialize(IEntitySerializer serializer) => serializer.Serialize(this);
        public void Deserialize(IEntitySerializer serializer, JToken token) => serializer.Deserialize(this, token);
    }
}